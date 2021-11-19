import { Request } from "express";
import UnauthenticatedError from "../errors/UnauthenticatedError";
const jwt = require("jsonwebtoken");

export const expressAuthentication = (
  req: Request,
  securityName: string,
  scopes?: string[]
): Promise<any> => {
  // Only auth jwt
  if (securityName !== "jwt") {
    return Promise.reject(null);
  }

  // Check authorization in header
  if (!req.headers || !req.headers.authorization) {
    return Promise.reject(new UnauthenticatedError("Authentication Invalid"));
  }

  // Verify jwt
  const token = req.headers.authorization.replace("Bearer ", "");
  return new Promise((resolve, reject) => {
    jwt.verify(token, process.env.JWT_SECRET, (err: any, decoded: any) => {
      // Handle error
      if (err) {
        reject(new UnauthenticatedError("Invalid JWT"));
        return;
      }

      // Validate scopes
      if (scopes) {
        for (let scope of scopes) {
          if (!decoded.scopes.includes(scope)) {
            reject(new UnauthenticatedError("Invalid Scope"));
            return;
          }
        }
      }

      // Save user to the request
      resolve(decoded);

      // todo?
      // req.user = {
      //   id: decoded.sub as string,
      //   username: decoded.Username as string,
      // };
    });
  });
};
