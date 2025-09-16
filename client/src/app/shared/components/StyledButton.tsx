import { Button,  styled, type ButtonProps } from "@mui/material";
import type { LinkProps } from "react-router";

type StyledButtonProp = ButtonProps & Partial<LinkProps>;

const styledButton = styled(Button)<StyledButtonProp> (({theme})=> ({
  '&.Mui-disabled': {
    backgroundColor: theme.palette.grey[500],
    color: theme.palette.text.disabled
  }
}))

export default styledButton;