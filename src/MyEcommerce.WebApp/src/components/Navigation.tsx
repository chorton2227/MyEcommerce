import React from "react";
import NextLink from "next/link";
import { AppBar, Link, Toolbar, Typography } from "@mui/material";
import jwtDecode, { JwtPayload } from "jwt-decode";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { logout, me } from "../apis/accountApi";

const Navigation: React.FC<{}> = () => {
  const { data: meResponse } = useQuery(["account", "me"], () => me());
  const queryClient = useQueryClient();
  const { mutate: logoutMutate } = useMutation(logout, {
    onSuccess: () => {
      queryClient.invalidateQueries(["account", "me"]);
    },
  });

  const decodedJwt = jwtDecode<JwtPayload>(meResponse?.jwt);

  const handleLogout = () => {
    logoutMutate();
  };

  return (
    <AppBar
      position="static"
      color="default"
      elevation={0}
      sx={{ borderBottom: (theme) => `1px solid ${theme.palette.divider}` }}
    >
      <Toolbar>
        <Typography variant="h6" color="inherit" noWrap sx={{ flexGrow: 1 }}>
          MyEcommerce
        </Typography>
        <NextLink href="/">
          <Link
            href="#"
            variant="button"
            color="text.primary"
            sx={{ my: 1, mx: 1.5 }}
          >
            Home
          </Link>
        </NextLink>
        {meResponse?.loggedIn ? (
          <React.Fragment>
            <Typography>{decodedJwt.username}</Typography>
            <Link
              href="#"
              variant="button"
              color="text.primary"
              sx={{ my: 1, mx: 1.5 }}
              onClick={handleLogout}
            >
              Logout
            </Link>
          </React.Fragment>
        ) : (
          <React.Fragment>
            <NextLink href="/account/register">
              <Link
                href="#"
                variant="button"
                color="text.primary"
                sx={{ my: 1, mx: 1.5 }}
              >
                Register
              </Link>
            </NextLink>
            <NextLink href="/account/login">
              <Link
                href="#"
                variant="button"
                color="text.primary"
                sx={{ my: 1, mx: 1.5 }}
              >
                Login
              </Link>
            </NextLink>
          </React.Fragment>
        )}
      </Toolbar>
    </AppBar>
  );
};

export default Navigation;
