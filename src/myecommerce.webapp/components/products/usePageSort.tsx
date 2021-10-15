import {
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  SelectChangeEvent,
} from "@mui/material";
import React, { ReactElement, useState } from "react";
import { ProductPageSortDto } from "typescript-axios-product-service";

const usePageSort = (): [ProductPageSortDto, ReactElement] => {
  const [pageSort, setPageSort] = useState(ProductPageSortDto.NUMBER_0);
  const pageSortComponent = (
    <React.Fragment>
      <FormControl fullWidth>
        <InputLabel id="product-list-page-sort-label">Sort</InputLabel>
        <Select
          labelId="product-list-page-sort-label"
          id="product-list-page-sort-select"
          value={pageSort}
          label="Sort"
          onChange={(e: SelectChangeEvent<ProductPageSortDto>) => {
            setPageSort(e.target.value as any);
          }}
        >
          <MenuItem value={ProductPageSortDto.NUMBER_0}>
            Default sorting
          </MenuItem>
          <MenuItem value={ProductPageSortDto.NUMBER_1}>
            Sort by newest
          </MenuItem>
          <MenuItem value={ProductPageSortDto.NUMBER_2}>
            Sort by price: low to high
          </MenuItem>
          <MenuItem value={ProductPageSortDto.NUMBER_3}>
            Sort by price: high to low
          </MenuItem>
        </Select>
      </FormControl>
    </React.Fragment>
  );
  return [pageSort, pageSortComponent];
};

export default usePageSort;
