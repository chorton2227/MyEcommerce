#!/usr/bin/env bash

echo Enter image version:
read VERSION

docker build -t chorton2227/myecommerce.services.paymentservice.api:$VERSION -f Dockerfile ../../
docker push chorton2227/myecommerce.services.paymentservice.api:$VERSION
ssh root@chorton.dev "
    docker pull chorton2227/myecommerce.services.paymentservice.api:$VERSION &&
    docker tag chorton2227/myecommerce.services.paymentservice.api:$VERSION dokku/payment-service.myecommerce:latest &&
    dokku deploy payment-service.myecommerce latest
"
