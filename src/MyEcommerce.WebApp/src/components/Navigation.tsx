import React, { useState } from "react";
import NextLink from "next/link";
import {
  AppBar,
  Button,
  Link,
  ListItemIcon,
  Menu,
  MenuItem,
  Toolbar,
  Typography,
} from "@mui/material";
import jwtDecode, { JwtPayload } from "jwt-decode";
import { useMutation, useQuery, useQueryClient } from "react-query";
import { logout, me } from "../apis/accountApi";
import ShoppingCart from "./shopping-cart/ShoppingCart";
import { KeyboardArrowDown, Logout, Receipt } from "@mui/icons-material";
import { useRouter } from "next/dist/client/router";

const Navigation: React.FC<{}> = () => {
  const [accountMenuAnchorEl, setAccountMenuAnchorEl] =
    useState<null | HTMLElement>(null);
  const isAccountMenuOpen = Boolean(accountMenuAnchorEl);

  const router = useRouter();

  const { data: meResponse } = useQuery(["account", "me"], () => me());
  const queryClient = useQueryClient();
  const { mutate: logoutMutate } = useMutation(logout, {
    onSuccess: () => {
      queryClient.invalidateQueries(["account", "me"]);
    },
  });

  const decodedJwt = meResponse?.loggedIn
    ? (jwtDecode<JwtPayload>(meResponse?.jwt as string) as any)
    : undefined;

  const handleLogout = () => {
    logoutMutate();
  };

  const handleAccountMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAccountMenuAnchorEl(event.currentTarget);
  };

  const handleAccountMenuClose = () => {
    setAccountMenuAnchorEl(null);
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
          <NextLink href="/">
            <Link href="#" color="text.primary" sx={{ textDecoration: "none" }}>
              MyEcommerce
            </Link>
          </NextLink>
        </Typography>
        {decodedJwt ? (
          <React.Fragment>
            <Button
              variant="text"
              onClick={handleAccountMenuOpen}
              aria-controls={isAccountMenuOpen ? "account-menu" : undefined}
              aria-haspopup="true"
              aria-expanded={isAccountMenuOpen ? "true" : "false"}
              endIcon={<KeyboardArrowDown />}
              sx={{ color: "text.primary" }}
            >
              {decodedJwt.Username}
            </Button>
            <Menu
              id="account-menu"
              anchorEl={accountMenuAnchorEl}
              open={isAccountMenuOpen}
              onClose={handleAccountMenuClose}
              onClick={handleAccountMenuClose}
              transformOrigin={{ horizontal: "right", vertical: "top" }}
              anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
            >
              <MenuItem onClick={() => router.push("/account/orders")}>
                <ListItemIcon>
                  <Receipt fontSize="small" />
                </ListItemIcon>
                View Orders
              </MenuItem>
              <MenuItem onClick={handleLogout}>
                <ListItemIcon>
                  <Logout fontSize="small" />
                </ListItemIcon>
                Logout
              </MenuItem>
            </Menu>
          </React.Fragment>
        ) : (
          <React.Fragment>
            <NextLink href="/account/register">
              <Link
                href="#"
                variant="button"
                color="text.primary"
                sx={{ my: 1, mx: 1.5, textDecoration: "none" }}
              >
                Register
              </Link>
            </NextLink>
            <NextLink href="/account/login">
              <Link
                href="#"
                variant="button"
                color="text.primary"
                sx={{ my: 1, mx: 1.5, textDecoration: "none" }}
              >
                Login
              </Link>
            </NextLink>
          </React.Fragment>
        )}
        <ShoppingCart />
      </Toolbar>
    </AppBar>
  );
};

export default Navigation;
