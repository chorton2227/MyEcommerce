import { Box, Typography } from "@mui/material";
import React from "react";

const Hero: React.FC<{}> = () => {
  return (
    <Box className="hero" sx={{ py: 12 }}>
      <Typography variant="h1">HUGE SALE</Typography>
      <Typography variant="h2">UP TO 70% OFF</Typography>
    </Box>
  );
};

export default Hero;
