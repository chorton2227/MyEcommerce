import {
  Alert,
  AlertTitle,
  Box,
  Container,
  Grid,
  Link,
  TextField,
  Typography,
} from "@mui/material";
import React, { useState } from "react";
import NextLink from "next/link";
import { register } from "../../apis/accountApi";
import { FieldErrorDto } from "../../generated/identity-service/dist";
import { useMutation, useQueryClient } from "react-query";
import { LoadingButton } from "@mui/lab";
import { useRouter } from "next/dist/client/router";
import { MeResponseDto } from "../../generated/identity-service/dist";

const Register = () => {
  const router = useRouter();
  const queryClient = useQueryClient();
  const [isError, setIsError] = useState(false);
  const [fieldErrors, setFieldErrors] = useState<Array<FieldErrorDto> | null>(
    null
  );

  const { isLoading, mutate: registerMutate } = useMutation(register, {
    onMutate: () => {
      setIsError(false);
      setFieldErrors(null);
    },
    onSuccess: (data) => {
      if (data.success) {
        queryClient.setQueryData(["account", "me"], {
          loggedIn: true,
          jwt: data.jwt,
        } as MeResponseDto);
        router.push("/");
      } else {
        setFieldErrors(data.fieldErrors || null);
      }
    },
    onError: (error, variables, context) => {
      setIsError(true);
    },
  });

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const data = new FormData(event.currentTarget);
    registerMutate({
      username: data.get("username")?.toString() || "",
      email: data.get("email")?.toString() || "",
      password: data.get("password")?.toString() || "",
      confirmPassword: data.get("confirmPassword")?.toString() || "",
    });
  };

  return (
    <Container component="main" maxWidth="xs">
      <Box
        sx={{
          mt: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Typography component="h1" variant="h4">
          Register
        </Typography>

        {!isError ? null : (
          <Alert severity="error" sx={{ mt: 2 }}>
            <AlertTitle>Server Error</AlertTitle>
          </Alert>
        )}

        {!fieldErrors ? null : (
          <Alert severity="error" sx={{ mt: 2 }}>
            <AlertTitle>The following errors occurred:</AlertTitle>
            {fieldErrors.map((fieldError) => {
              return (
                <Typography key={fieldError.field}>
                  {fieldError.message}
                </Typography>
              );
            })}
          </Alert>
        )}

        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 4 }}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <TextField
                name="username"
                required
                fullWidth
                id="username"
                label="Username"
                autoFocus
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                name="email"
                required
                fullWidth
                id="email"
                label="Email"
                autoComplete="email"
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                name="password"
                required
                fullWidth
                id="password"
                label="Password"
                type="password"
                autoComplete="password"
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                name="confirmPassword"
                required
                fullWidth
                id="confirmPassword"
                label="Confirm Password"
                type="password"
                autoComplete="password"
              />
            </Grid>
          </Grid>
          <LoadingButton
            type="submit"
            fullWidth
            variant="contained"
            sx={{ my: 2 }}
            loading={isLoading}
          >
            Register
          </LoadingButton>
          <Grid container>
            <Grid item sx={{ mx: "auto" }}>
              <NextLink href="/account/login">
                <Link href="#">Already have an account? Login</Link>
              </NextLink>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
};

export default Register;
