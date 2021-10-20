#!/usr/bin/env bash

echo Enter image version:
read VERSION

docker build -t chorton2227/myecommerce.services.productservice.api:$VERSION -f Dockerfile ../../
docker push chorton2227/myecommerce.services.productservice.api:$VERSION
ssh root@chorton.dev "
    docker pull chorton2227/myecommerce.services.productservice.api:$VERSION &&
    docker tag chorton2227/myecommerce.services.productservice.api:$VERSION dokku/product-service.myecommerce:latest &&
    dokku deploy product-service.myecommerce latest
"
