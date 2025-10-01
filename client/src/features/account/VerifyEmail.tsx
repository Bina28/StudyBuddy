import { useEffect, useRef, useState } from "react";
import { useAccount } from "../../lib/hooks/useAccount";
import { Link, useSearchParams } from "react-router";
import { Box, Button, Divider, Paper, Typography } from "@mui/material";
import { EmailRounded } from "@mui/icons-material";

export default function VerifyEmail() {
  const { vertifyEmail, resendConfiramtionEmail } = useAccount();
  const [status, setStatus] = useState("vertifying");
  const [searchParams] = useSearchParams();
  const userId = searchParams.get("userId");
  const code = searchParams.get("code");
  const hasRun = useRef(false);

  useEffect(() => {
    if (code && userId && !hasRun.current) {
      hasRun.current = true;
      vertifyEmail
        .mutateAsync({ userId, code })
        .then(() => setStatus("verified"))
        .catch(() => setStatus("failed"));
    }
  }, [code, userId, vertifyEmail]);

  const getBody = () => {
    switch (status) {
      case "verifying":
        return <Typography>Verifying...</Typography>;
      case "failed":
        return (
          <Box
            display="flex"
            flexDirection="column"
            gap={2}
            justifyContent="center"
          >
            <Typography>
              Vertification failed. You can try resending the vertify link to
              your email
            </Typography>
            <Button
              onClick={() => resendConfiramtionEmail.mutate({ userId })}
              disabled={resendConfiramtionEmail.isPending}
            >
              Resend vertification email
            </Button>
          </Box>
        );
      case "verified":
        return (
          <Box
            display="flex"
            flexDirection="column"
            gap={2}
            justifyContent="center"
          >
            <Typography>Email has been verified - you can now login</Typography>
            <Button component={Link} to="/login">
              Go to login
            </Button>
          </Box>
        );
    }
  };

  return (
    <Paper
      sx={{
        height: 400,
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        p: 6,
      }}
    >
      <EmailRounded sx={{ fontSize: 100 }} color="primary"></EmailRounded>
      <Typography gutterBottom variant="h3">
        Email vertification
      </Typography>
      <Divider />
      {getBody()}
    </Paper>
  );
}
