#!/usr/bin/env bash

echo Enter image version:
read VERSION

# Build and push images
docker build -t chorton2227/myecommerce.services.productservice.api:$VERSION -f src/MyEcommerce.Services.ProductService.API/Dockerfile .
docker push chorton2227/myecommerce.services.productservice.api:$VERSION

# Deploy images to production
ssh root@chorton.dev "
    docker pull chorton2227/myecommerce.services.productservice.api:$VERSION &&
    docker tag chorton2227/myecommerce.services.productservice.api:$VERSION dokku/product-service.myecommerce:$VERSION &&
    dokku deploy product-service.myecommerce $VERSION
"
