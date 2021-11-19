#!/usr/bin/env bash

echo Enter image version:
read VERSION

docker build -t chorton2227/myecommerce.services.shoppingcartservice.api:$VERSION -f Dockerfile .
docker push chorton2227/myecommerce.services.shoppingcartservice.api:$VERSION
ssh root@chorton.dev "
    docker pull chorton2227/myecommerce.services.shoppingcartservice.api:$VERSION &&
    docker tag chorton2227/myecommerce.services.shoppingcartservice.api:$VERSION dokku/shopping-cart-service.myecommerce:latest &&
    dokku deploy myecommerce latest
"
