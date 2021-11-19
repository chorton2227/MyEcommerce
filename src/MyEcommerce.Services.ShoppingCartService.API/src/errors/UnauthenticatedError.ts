import ApiError from "./ApiError";
import { StatusCodes } from "http-status-codes";

export default class UnauthenticatedError extends ApiError {
  constructor(message: string) {
    super(message);
    this.statusCode = StatusCodes.UNAUTHORIZED;
  }
}
