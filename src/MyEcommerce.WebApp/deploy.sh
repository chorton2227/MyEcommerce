#!/usr/bin/env bash

echo Enter image version:
read VERSION

docker build -t chorton2227/myecommerce.webapp:$VERSION -f Dockerfile .
docker push chorton2227/myecommerce.webapp:$VERSION
ssh root@chorton.dev "
    docker pull chorton2227/myecommerce.webapp:$VERSION &&
    docker tag chorton2227/myecommerce.webapp:$VERSION dokku/myecommerce:latest &&
    dokku deploy myecommerce latest
"
