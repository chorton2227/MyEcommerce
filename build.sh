#!/usr/bin/env bash

# Build and push docker files
docker build -t chorton2227/myecommerce.services.productservice.api -f src/MyEcommerce.Services.ProductService.API/Dockerfile .
docker push chorton2227/myecommerce.services.productservice.api
