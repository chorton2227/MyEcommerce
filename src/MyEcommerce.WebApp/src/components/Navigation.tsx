import React from "react";
import NextLink from "next/link";
import { AppBar, Link, Toolbar, Typography } from "@mui/material";

const Navigation: React.FC<{}> = () => {
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
        <nav>
          <NextLink href="/">
            <Link variant="button" color="text.primary" sx={{ my: 1, mx: 1.5 }}>
              Home
            </Link>
          </NextLink>
        </nav>
      </Toolbar>
    </AppBar>
  );
};

export default Navigation;
