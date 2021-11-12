import { Box, Slider, Typography } from "@mui/material";
import React, { ReactElement, useState } from "react";

const usePriceFilter = (): [number, number, ReactElement] => {
  const [priceRange, setPriceRange] = useState<number[]>([1, 100]);
  const [minPrice, setMinPrice] = useState<number>(1);
  const [maxPrice, setMaxPrice] = useState<number>(100);
  const priceFilterComponent = (
    <React.Fragment>
      <Typography variant="h6" mt={2} mb={1}>
        Price
      </Typography>
      <Box ml={2} mr={4} mt={5}>
        <Slider
          sx={{}}
          getAriaLabel={() => "Price range filter"}
          value={priceRange}
          valueLabelDisplay="on"
          onChange={(_: Event, newValue: number | number[]) => {
            setPriceRange(newValue as number[]);
          }}
          onChangeCommitted={(
            _: React.SyntheticEvent | Event,
            newValue: number | number[]
          ) => {
            const newPriceRange = newValue as number[];
            setMinPrice(newPriceRange[0]);
            setMaxPrice(newPriceRange[1]);
          }}
          getAriaValueText={(value: number) => {
            return `$${value}`;
          }}
        />
      </Box>
    </React.Fragment>
  );
  return [minPrice, maxPrice, priceFilterComponent];
};

export default usePriceFilter;
