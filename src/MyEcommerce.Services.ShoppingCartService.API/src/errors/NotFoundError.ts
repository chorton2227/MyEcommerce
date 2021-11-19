import ApiError from "./ApiError";
import { StatusCodes } from "http-status-codes";

export default class NotFoundError extends ApiError {
  constructor(message: string) {
    super(message);
    this.statusCode = StatusCodes.NOT_FOUND;
  }
}
