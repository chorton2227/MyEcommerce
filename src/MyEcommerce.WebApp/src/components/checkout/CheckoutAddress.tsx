import { Box, Grid, TextField, Typography } from "@mui/material";
import React, { forwardRef, ReactElement, useImperativeHandle } from "react";
import { InputField, useFormControls } from "../../utils/useFormControls";

const inputFields: InputField[] = [
  {
    id: "firstName",
    name: "firstName",
    label: "First Name",
    autoComplete: "given-name",
    defaultValue: "",
    required: true,
    gridXs: 12,
    gridSm: 6,
  },
  {
    id: "lastName",
    name: "lastName",
    label: "Last Name",
    autoComplete: "family-name",
    defaultValue: "",
    required: true,
    gridXs: 12,
    gridSm: 6,
  },
  {
    id: "address1",
    name: "address1",
    label: "Address Line 1",
    autoComplete: "shipping address-line1",
    defaultValue: "",
    required: true,
    gridXs: 12,
    gridSm: undefined,
  },
  {
    id: "address2",
    name: "address2",
    label: "Address Line 2",
    autoComplete: "shipping address-line2",
    defaultValue: "",
    required: false,
    gridXs: 12,
    gridSm: undefined,
  },
  {
    id: "city",
    name: "city",
    label: "City",
    autoComplete: "shipping city",
    defaultValue: "",
    required: true,
    gridXs: 12,
    gridSm: 6,
  },
  {
    id: "state",
    name: "state",
    label: "State",
    autoComplete: "shipping state",
    defaultValue: "",
    required: true,
    gridXs: 12,
    gridSm: 6,
  },
  {
    id: "country",
    name: "country",
    label: "Country",
    autoComplete: "shipping country",
    defaultValue: "",
    required: true,
    gridXs: 12,
    gridSm: 6,
  },
  {
    id: "zip",
    name: "zip",
    label: "Zip / Postal Code",
    autoComplete: "shipping postal-code",
    defaultValue: "",
    required: true,
    gridXs: 12,
    gridSm: 6,
  },
];

export interface DeliveryAddress {
  firstName: string;
  lastName: string;
  address1: string;
  address2: string;
  city: string;
  state: string;
  country: string;
  zip: string;
}

export interface CheckoutAddressFunctions {
  submitForm(e: any): DeliveryAddress | undefined;
}

export interface CheckoutAddressProps {}

const CheckoutAddress = forwardRef<
  CheckoutAddressFunctions,
  CheckoutAddressProps
>(({}, ref): ReactElement => {
  const { handleInputValue, handleFormSubmit, errors, values } =
    useFormControls({
      inputFields,
    });

  useImperativeHandle(ref, () => ({
    submitForm(e: any) {
      if (!handleFormSubmit(e)) {
        return undefined;
      }

      return values;
    },
  }));

  return (
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Delivery Address
      </Typography>
      <Grid container spacing={3}>
        {inputFields.map((field, index) => (
          <Grid item key={index} xs={field.gridXs} sm={field.gridSm}>
            <TextField
              fullWidth
              variant="standard"
              id={field.id}
              name={field.name}
              label={field.label}
              autoComplete={field.autoComplete}
              required={field.required}
              onBlur={handleInputValue}
              onChange={handleInputValue}
              {...(errors[field.name] && {
                error: true,
                helperText: errors[field.name],
              })}
            />
          </Grid>
        ))}
      </Grid>
    </React.Fragment>
  );
});

export default CheckoutAddress;
