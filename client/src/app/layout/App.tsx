import { useEffect, useState } from "react";
import axios from "axios";
import { Box, Container } from "@mui/material";
import NavBar from "./NavBar";
import ActivityDashboard from "../../features/activties/dashboard/ActivityDashboard";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<
    Activity | undefined
  >(undefined);
  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    axios
      .get<Activity[]>("https://localhost:5001/api/activities")
      .then((response) => setActivities(response.data));
  }, []);

  const handleSelectedActivity = (id: string) => {
    setSelectedActivity(activities.find((x) => x.id === id));
  };

  const handleCancelSelectedActivity = () => {
    setSelectedActivity(undefined);
  };

  const handleOpenForm = (id?: string) => {
    if (id) {
      handleSelectedActivity(id);
    } else {
      handleCancelSelectedActivity();
    }
    setEditMode(true);
  };

  const handleFormClose = () => {
    setEditMode(false);
  };

  const handleSubmitForm = (activity: Activity) => {
    if (activity.id) {
      setActivities(
        activities.map((x) => (x.id === activity.id ? activity : x))
      );
    } else {
      const newActivity = { ...activity, id: activities.length.toString() };
      setSelectedActivity(newActivity);
      setActivities([...activities, newActivity]);
    }
    setEditMode(false);
      setSelectedActivity(undefined); 
  };

  const handleDelete = (id:string) =>{
    setActivities(activities.filter(x => x.id!==id))
  }

  return (
    <Box sx={{ bgcolor: "#eeeeee" }}>
      <NavBar openForm={handleOpenForm} />
      <Container maxWidth="xl" sx={{ mt: 3 }}>
        <ActivityDashboard
          activities={activities}
          selectActivity={handleSelectedActivity}
          cancelSelectedActivity={handleCancelSelectedActivity}
          selectedActivity={selectedActivity}
          editMode={editMode}
          openForm={handleOpenForm}
          closeForm={handleFormClose}
          submitForm={handleSubmitForm}
          deleteActivity={handleDelete}
        />
      </Container>
    </Box>
  );
}

export default App;
