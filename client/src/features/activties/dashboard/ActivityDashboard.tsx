import { Grid2 } from "@mui/material";
import ActivityList from "./ActivityList";
import ActivityDetail from "../details/ActivityDetail";
import ActivityForm from "../form/ActivityForm";

type Props = {
  activities: Activity[];
  selectActivity: (id: string) => void;
  cancelSelectedActivity: () => void;
  selectedActivity?: Activity | undefined;
  editMode: boolean;
  openForm: (id: string) => void;
  closeForm: () => void;
  submitForm: (activity:Activity) => void;
  deleteActivity: (id:string)=> void;
};

export default function ActivityDashboard({
  activities,
  cancelSelectedActivity,
  selectActivity,
  selectedActivity,
  editMode,
  openForm,
  closeForm,
  submitForm,
  deleteActivity
}: Props) {
  return (
    <Grid2 container spacing={5}>
      <Grid2 size={7}>
        <ActivityList deleteActivity={deleteActivity} activites={activities} selectActivity={selectActivity} />
      </Grid2>
      <Grid2 size={5}>
        {selectedActivity && !editMode && 
          <ActivityDetail
            activity={selectedActivity}
            cancelSelectActivity={cancelSelectedActivity}
            openForm={openForm}
          />
        }
      {editMode && <ActivityForm  submitForm={submitForm} closeForm={closeForm} activity={selectedActivity}/>}
      </Grid2>
    </Grid2>
  );
}
