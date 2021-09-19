#!/usr/bin/env bash

# Apply nginx ingress controller
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.0/deploy/static/provider/cloud/deploy.yaml

# Apply k8s
kubectl apply -f ./k8s/myecommerce-namespace.yaml
kubectl apply -f ./k8s/ingress-srv.yaml
kubectl apply -f ./k8s/productservice-mssql-deploy.yaml
kubectl apply -f ./k8s/productservice-deploy.yaml

# Restart deployments
kubectl rollout restart deployment -n myecommerce productservice-deploy