import { GridSize } from "@mui/material";
import { useState } from "react";

export interface FormControlsProps {
  inputFields: InputField[];
}

export type InputField = {
  id: string;
  name: string;
  label: string;
  autoComplete: string | undefined;
  defaultValue: string | undefined;
  required: boolean | undefined;
  gridXs: boolean | GridSize | undefined;
  gridSm: boolean | GridSize | undefined;
};

export const useFormControls = ({ inputFields }: FormControlsProps) => {
  // State of form controls
  const [values, setValues] = useState(
    inputFields.reduce(
      (obj, field) => Object.assign(obj, { [field.id]: field.defaultValue }),
      {} // initial object as empty object
    ) as any
  );
  // State of form errors
  const [errors, setErrors] = useState({} as any);

  // Validates form based on values, updates errors
  const validateForm = (fieldValues = values) => {
    let newErrors: any = { ...errors };

    // Validate required fields
    for (let i = 0; i < inputFields.length; i++) {
      const field = inputFields[i];
      if (field.required && field.name in fieldValues) {
        newErrors[field.name] = fieldValues[field.name]
          ? ""
          : "This field is required";
      }
    }

    setErrors(newErrors);
  };

  // Handles form control input
  const handleInputValue = (e: any) => {
    const { name, value } = e.target;
    setValues({
      ...values,
      [name]: value,
    });
    validateForm({ [name]: value });
  };

  // Handles form submission, requires valid form
  const handleFormSubmit = (e: any, callback?: () => void): boolean => {
    e.preventDefault();

    const isValid = isFormValid();

    if (isValid && callback) {
      callback();
    }

    return isValid;
  };

  // Final check for form
  const isFormValid = (fieldValues = values): boolean => {
    // Check for all required fields
    for (let i = 0; i < inputFields.length; i++) {
      const field = inputFields[i];
      if (
        field.required &&
        field.name in fieldValues &&
        !fieldValues[field.name]
      ) {
        return false;
      }
    }

    // Check for any other errors
    return Object.values(errors).every((x) => x === "");
  };

  return {
    handleInputValue,
    handleFormSubmit,
    isFormValid,
    errors,
    values,
  };
};
