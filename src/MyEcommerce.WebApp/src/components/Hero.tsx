import { Box, Typography } from "@mui/material";
import React from "react";

const Hero: React.FC<{}> = () => {
  return (
    <Box className="hero" sx={{ py: 6 }}>
      <Typography variant="h2" component="h1">
        HUGE SALE
      </Typography>
      <Typography variant="h3" component="h2">
        UP TO 70% OFF
      </Typography>
    </Box>
  );
};

export default Hero;
