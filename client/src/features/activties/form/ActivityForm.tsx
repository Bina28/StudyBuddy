import { Box, Button, Paper, Typography } from "@mui/material";
import { useActivities } from "../../../lib/hooks/useActivities";
import { useNavigate, useParams } from "react-router";
import { useForm, type Resolver } from "react-hook-form";
import { useEffect } from "react";
import {
  activytSchema,
  type ActivitySchema,
} from "../../../lib/schemas/activitySchema";
import { zodResolver } from "@hookform/resolvers/zod";
import TextInput from "../../../app/shared/components/TextInput";
import SelectInput from "../../../app/shared/components/SelectInput";
import { categoryOptions } from "./categoryOptions";
import DateTimeInput from "../../../app/shared/components/DateTimeInput";
import LocationInput from "../../../app/shared/components/LocationInput";

export default function ActivityForm() {
  const { control, reset, handleSubmit } = useForm<ActivitySchema>({
    mode: "onTouched",
    resolver: zodResolver(activytSchema) as Resolver<ActivitySchema>,
  });
  const { id } = useParams();
  const { updateActivity, createActivity, activity, isLoadingActivity } =
    useActivities(id);
  const navigate = useNavigate();

  useEffect(() => {
    if (activity)
      reset({
        ...activity,
        location: {  
          locationName: activity.locationName,
          latitude: activity.latitude,
          longitude: activity.longitude,
        },
      });
  }, [activity, reset]);

  const onSubmit = async (data: ActivitySchema) => {
    const { location, ...rest } = data;
    const flattenedData = { ...rest, ...location };

    try {
      if (activity) {
        updateActivity.mutate(
          { ...activity, ...flattenedData },
          {
            onSuccess: () => navigate(`/activities/${activity.id}`),
          }
        );
      } else {
        createActivity.mutate(flattenedData, {
          onSuccess: (id) => navigate(`/activities/${id}`),
        });
      }
    } catch (error) {
      console.log(error);
    }
  };

  if (isLoadingActivity) return <Typography>Loading...</Typography>;

  return (
    <Paper elevation={3} sx={{ p: 3, borderRadius: 2 }}>
      <Typography variant="h5" gutterBottom color="primary">
        {activity ? "Edit activity" : "Create activity"}
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit(onSubmit)}
        display="flex"
        flexDirection="column"
        gap={2}
      >
        <TextInput label="Title" control={control} name="title" />

        <TextInput
          label="Description"
          control={control}
          name="description"
          multiline
          rows={3}
        />
         <TextInput
          label="University"
          control={control}
          name="university"
          multiline
          rows={2}
        />
                 <TextInput
          label="City"
          control={control}
          name="city"
     
        />
        <Box display="flex" gap={3}>
          <SelectInput
            items={categoryOptions}
            label="Category"
            control={control}
            name="category"
          />
          <DateTimeInput label="Date" control={control} name="date" />
        </Box>

        <LocationInput
          control={control}
          label="Enter the location"
          name="location"
        />

        <Box display="flex" justifyContent="end" gap={3}>
          <Button color="inherit" onClick={()=>navigate(-1)}>Cancel</Button>
          <Button
            type="submit"
            variant="contained"
            color="success"
            disabled={updateActivity.isPending || createActivity.isPending}
          >
            Submit
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}
