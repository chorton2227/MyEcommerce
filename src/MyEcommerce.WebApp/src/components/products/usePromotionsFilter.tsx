import {
  FormGroup,
  FormControlLabel,
  Checkbox,
  Typography,
} from "@mui/material";
import React, { ReactElement, useState } from "react";

const usePromotionFilters = (): [boolean, boolean, ReactElement] => {
  const [isNew, setIsNew] = useState(false);
  const [onSale, setOnSale] = useState(false);
  const promotionsFilterComponent = (
    <React.Fragment>
      <Typography variant="h6" mt={2} mb={1}>
        Promotion
      </Typography>
      <FormGroup>
        <FormControlLabel
          label="Is New"
          control={
            <Checkbox
              checked={isNew}
              onChange={(e: any) => {
                setIsNew(e.target.checked);
              }}
            />
          }
        />
        <FormControlLabel
          label="On Sale"
          control={
            <Checkbox
              checked={onSale}
              onChange={(e: any) => {
                setOnSale(e.target.checked);
              }}
            />
          }
        />
      </FormGroup>
    </React.Fragment>
  );
  return [isNew, onSale, promotionsFilterComponent];
};

export default usePromotionFilters;
