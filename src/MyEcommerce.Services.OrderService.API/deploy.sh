#!/usr/bin/env bash

echo Enter image version:
read VERSION

docker build -t chorton2227/myecommerce.services.orderservice.api:$VERSION -f Dockerfile ../../
docker push chorton2227/myecommerce.services.orderservice.api:$VERSION
ssh root@chorton.dev "
    docker pull chorton2227/myecommerce.services.orderservice.api:$VERSION &&
    docker tag chorton2227/myecommerce.services.orderservice.api:$VERSION dokku/order-service.myecommerce:latest &&
    dokku deploy order-service.myecommerce latest
"
