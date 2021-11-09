#!/usr/bin/env bash

echo Enter image version:
read VERSION

docker build -t chorton2227/myecommerce.services.identityservice.api:$VERSION -f Dockerfile ../../
docker push chorton2227/myecommerce.services.identityservice.api:$VERSION
ssh root@chorton.dev "
    docker pull chorton2227/myecommerce.services.identityservice.api:$VERSION &&
    docker tag chorton2227/myecommerce.services.identityservice.api:$VERSION dokku/identity-service.myecommerce:latest &&
    dokku deploy identity-service.myecommerce latest
"
