import {
  useController,
  type FieldValues,
  type UseControllerProps,
} from "react-hook-form";
import { DateTimePicker, type DateTimeFieldProps } from "@mui/x-date-pickers";

type Props<T extends FieldValues> = {} & UseControllerProps<T> &
 DateTimeFieldProps

export default function DateTimeInput<T extends FieldValues>(props: Props<T>) {
  const { field, fieldState } = useController({ ...props });
  return (
    <DateTimePicker
      {...props}

     value={field.value ?? null}
  onChange={value => field.onChange(value ?? null)}
      sx={{ width: "100%" }}
      slotProps={{
        textField: {
          onBlur:field.onBlur,
          error: !!fieldState.error,
          helperText: fieldState.error?.message
        },
      }}
    />
  );
}
