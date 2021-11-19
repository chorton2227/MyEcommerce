import "dotenv-safe/config";
import cors from "cors";
import express from "express";
import path from "path";
import { createConnection } from "typeorm";
import { __prod__ } from "./constants";
import { ShoppingCart } from "./data/entities/ShoppingCart";
import { ShoppingCartItem } from "./data/entities/ShoppingCartItem";
import errorHandlerMiddleware from "./middlewares/errorHandlerMiddleware";
import bodyParser from "body-parser";
import { RegisterRoutes } from "./gen/routes";
import notFoundMiddleware from "./middlewares/notFoundMiddleware";

const main = async () => {
  // Connect to database and run migrations
  const db = await createConnection({
    type: "postgres",
    url: process.env.DATABASE_URL,
    logging: !__prod__, // disable logging in prod
    entities: [ShoppingCart, ShoppingCartItem],
    migrations: [path.join(__dirname, "./data/migrations/*")],
    migrationsTableName: "shopping_cart_migrations",
  });
  await db.runMigrations();

  // Express server
  const app = express();

  // Required for production
  // https://expressjs.com/en/guide/behind-proxies.html
  app.set("trust proxy", 1);

  // Enable cors on all requests
  app.use(
    cors({
      origin: process.env.CORS_ORIGIN,
      credentials: true,
    })
  );

  // Use body parser to read sent json payloads
  app.use(
    bodyParser.urlencoded({
      extended: true,
    })
  );
  app.use(bodyParser.json());

  // Parse application/x-www-form-urlencoded requests
  app.use(
    express.urlencoded({
      extended: true,
    })
  );

  // Register tsoa routes
  RegisterRoutes(app);

  // Serve swagger file
  app.use("/swagger", express.static(path.join(__dirname, "swagger")));

  // API Routes
  // app.use("/api/v1/shopping-carts", shoppingCartRouter);

  // Middleware
  app.use(notFoundMiddleware);
  app.use(errorHandlerMiddleware);

  // Start listening
  app.listen(process.env.PORT, () => {
    console.log(`server is listening on port ${process.env.PORT}...`);
  });
};

main().catch((err) => {
  console.error(err);
});
