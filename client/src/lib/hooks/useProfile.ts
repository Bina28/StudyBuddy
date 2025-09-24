import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../api/agent";
import { useMemo } from "react";

export const useProfile = (id?: string, predicate?: string) => {
  const queryClinet = useQueryClient();

  const { data: profile, isLoading: loadingProfile } = useQuery<Profile>({
    queryKey: ["profile", id],
    queryFn: async () => {
      const response = agent.get<Profile>(`/profiles/${id}`);
      return (await response).data;
    },
    enabled: !!id && !predicate,
  });

  const { data: photos, isLoading: loadingPhotos } = useQuery<Photo[]>({
    queryKey: ["photos", id],
    queryFn: async () => {
      const response = agent.get<Photo[]>(`/profiles/${id}/photos`);
      return (await response).data;
    },
    enabled: !!id && !predicate,
  });

  const { data: followings, isLoading: loadingFollowings } = useQuery<
    Profile[]
  >({
    queryKey: ["followings", id, predicate],
    queryFn: async () => {
      const response = await agent.get<Profile[]>(
        `/profiles/${id}/follow-list?predicate=${predicate}`
      );
      return response.data;
    },
    enabled: !!id && !!predicate,
  });

  const uploadPhoto = useMutation({
    mutationFn: async (file: Blob) => {
      const formData = new FormData();
      formData.append("file", file);
      const response = await agent.post("/profiles/add-photo", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
      return response.data;
    },
    onSuccess: async (photo: Photo) => {
      await queryClinet.invalidateQueries({
        queryKey: ["photo", id],
      });
      queryClinet.setQueryData(["user"], (data: User) => {
        if (!data) return data;
        return {
          ...data,
          imageUrl: data.imageUrl ?? photo.url,
        };
      });
      queryClinet.setQueryData(["profile", id], (data: Profile) => {
        if (!data) return data;
        return {
          ...data,
          imageUrl: data.imageUrl ?? photo.url,
        };
      });
    },
  });

  const setMainPhoto = useMutation({
    mutationFn: async (photo: Photo) => {
      await agent.put(`/profiles/${photo.id}/setMain`);
    },
    onSuccess: (_, photo) => {
      queryClinet.setQueryData<User>(["user"], (userData) => {
        if (!userData) return userData;
        return {
          ...userData,
          imageUrl: photo.url,
        };
      });

      queryClinet.setQueryData<Profile>(["profile", id], (profileData) => {
        if (!profileData) return profileData;
        return {
          ...profileData,
          imageUrl: photo.url,
        };
      });
    },
  });

  const deleltePhoto = useMutation({
    mutationFn: async (photoId: string) => {
      await agent.delete(`/profiles/${photoId}/photos`);
    },
    onSuccess: (_, photoId) => {
      queryClinet.setQueryData(["photos", id], (photos: Photo[]) => {
        return photos.filter((x) => x.id !== photoId);
      });
    },
  });

  const updateFollowing = useMutation({
    mutationFn: async () => {
      await agent.post(`/profiles/${id}/follow`);
    },
    onSuccess: () => {
      queryClinet.setQueryData(["profile", id], (profile: Profile) => {
        queryClinet.invalidateQueries({
          queryKey: ["followings", id, "followers"],
        });
        if (!profile || profile.followersCount === undefined) return profile;
        return {
          ...profile,
          following: !profile.following,
          followersCount: profile.following
            ? profile.followersCount - 1
            : profile.followersCount + 1,
        };
      });
    },
  });

  const isCurrentUser = useMemo(() => {
    return id === queryClinet.getQueryData<User>(["user"])?.id;
  }, [id, queryClinet]);

  return {
    profile,
    loadingProfile,
    photos,
    loadingPhotos,
    isCurrentUser,
    uploadPhoto,
    setMainPhoto,
    deleltePhoto,
    updateFollowing,
    followings,
    loadingFollowings,
  };
};
