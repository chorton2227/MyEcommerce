import { Container, Typography } from "@mui/material";
import React from "react";

const Footer: React.FC<{}> = () => {
  return (
    <footer>
      <Container>
        <Typography variant="body2">&copy; 2021 MyEcommerce</Typography>
      </Container>
    </footer>
  );
};

export default Footer;
