import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import type { FormEvent } from "react";

type Props = {
  activity?: Activity;
  closeForm: () => void;
  submitForm: (activity: Activity) => void;
};

export default function ActivityForm({
  activity,
  closeForm,
  submitForm,
}: Props) {
  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);
    const data: { [key: string]: FormDataEntryValue } = {};
    formData.forEach((value, key) => {
      data[key] = value;

      if (activity) data.id = activity.id;
    });
    submitForm(data as unknown as Activity);
  };

  return (
    <Paper elevation={3} sx={{ p: 3, borderRadius: 2 }}>
      <Typography variant="h5" gutterBottom color="primary">
        Create activity
      </Typography>
      <Box
        component="form"
        onSubmit={handleSubmit}
        display="flex"
        flexDirection="column"
        gap={2}
      >
        <TextField name="title" label="Title" defaultValue={activity?.title} />
        <TextField
          name="date"
          label="Date"
          defaultValue={activity?.date}
          type="date"
        />
        <TextField
          name="description"
          label="Description"
          defaultValue={activity?.description}
          multiline
          rows={3}
        />
        <TextField
          name="category"
          label="Category"
          defaultValue={activity?.category}
        />
        <TextField name="city" label="City" defaultValue={activity?.city} />
        <TextField
          name="univesity"
          label="University"
          defaultValue={activity?.university}
        />
        <TextField
          name="locationName"
          label="Location Name"
          defaultValue={activity?.locationName}
        />
        <Box display="flex" justifyContent="end" gap={3}>
          <Button color="inherit" onClick={closeForm}>
            Cancel
          </Button>
          <Button type="submit" variant="contained" color="success">
            Submit
          </Button>
        </Box>
      </Box>
    </Paper>
  );
}
