type PageList<T, TCursor> = {
  items: T[];
  nextCursor: TCursor;
};

type Activity = {
  id: string;
  title: string;
  date: string;
  description: string;
  category: string;
  isCancelled: boolean;
  city: string;
  university: string;
  locationName: string;
  latitude: number;
  longitude: number;
  attendees: Profile[];
  isGoing: boolean;
  isHost: boolean;
  hostId: string;
  hostDisplayName: string;
  hostImageUrl?: string;
};

type Profile = {
  id: string;
  displayName: string;
  bio?: string;
  imageUrl?: string;
  followersCount?: number;
  followingCount?: number;
  following?: boolean;
};

type Photo = {
  id: string;
  url: string;
};

type User = {
  id: string;
  email: string;
  displayName: string;
  imageUrl?: string;
};

type ChatComment = {
  id: string;
  createdAt: Date;
  body: string;
  userId: sting;
  displayName: string;
  imageUrl?: string;
};

type LocationIQSuggestion = {
  place_id: string;
  licence: string;
  osm_type: string;
  osm_id: string;
  lat: string;
  lon: string;
  display_name: string;
  address: LocationIQAddress;
  boundingbox: string[];
};

type LocationIQAddress = {
  attraction: string;
  house_number: string;
  road: string;
  quarter: string;
  suburb: string;
  city?: string;
  town?: string;
  village?: string;
  state_district: string;
  state: string;
  postcode: string;
  country: string;
  country_code: string;
};
