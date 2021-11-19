import { Request, Response, ErrorRequestHandler, NextFunction } from "express";
import { StatusCodes } from "http-status-codes";
import { ValidateError } from "tsoa";
import ApiError from "../errors/ApiError";

const errorHandlerMiddleware: ErrorRequestHandler = (
  err: unknown,
  req: Request,
  res: Response,
  next: NextFunction
): Response | void => {
  const defaultStatusCode = StatusCodes.INTERNAL_SERVER_ERROR;
  const defaultMessage = "Internal Server Error";

  if (err instanceof ApiError) {
    console.warn(`Caught ApiError for ${req.path}`, err);
    const statusCode = err.statusCode || defaultStatusCode;
    const message = err.message;
    return res.status(statusCode).json({ message });
  }

  if (err instanceof ValidateError) {
    console.warn(`Caught ValidateError for ${req.path}`, err);
    return res.status(StatusCodes.UNPROCESSABLE_ENTITY).json({
      message: "Validation Failed",
      details: err?.fields,
    });
  }

  if (err instanceof Error) {
    console.warn(`Caught Error for ${req.path}`, err);
    return res.status(defaultStatusCode).json({ message: defaultMessage });
  }

  next();
};

export default errorHandlerMiddleware;
