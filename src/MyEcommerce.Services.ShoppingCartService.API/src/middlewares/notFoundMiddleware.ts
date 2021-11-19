import { Request, Response } from "express";
import { StatusCodes } from "http-status-codes";

const notFoundMiddleware = (_req: Request, res: Response) => {
  res.status(StatusCodes.NOT_FOUND).send({ message: "Not Found" });
};

export default notFoundMiddleware;
