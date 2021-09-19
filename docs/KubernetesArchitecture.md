# Kubernetes Architecture

```plantuml
@startuml
    left to right direction

    node "Clients" as clients {

    }

    together "Cluster" as cluster {
        node "Node" as node {
            rectangle "Ingress Nginx Load Balancer" as ingressNginxLoadBalancer {
                rectangle "Port 80" as ingressNginxLoadBalancerPort80 {
                }
            }

            rectangle "Pod" as ingressNginxPod {
                rectangle "Ingress Nginx Container" as ingressNginxContainer {
                }
                rectangle "Port 80" as ingressNginxPort80 {
                }
            }

            rectangle "Pod" as rabbitMqPod {
                rectangle "RabbitMQ Container" as rabbitMqContainer {
                }
                rectangle "Port 5672" as rabbitMqPort5672 {
                }
                rectangle "Port 15672" as rabbitMqPort15672 {
                }
            }

            rectangle "Cluster IP" as rabbitMqClusterIp {
                rectangle "Port 5672" as rabbitMqClusterIpPort5672 {
                }
                rectangle "Port 15672" as rabbitMqClusterIpPort15672 {
                }
            }

            rectangle "Pod" as seqPod {
                rectangle "Seq Container" as seqContainer {
                }
                rectangle "Port 80" as seqPort80 {
                }
            }

            rectangle "Cluster IP" as seqClusterIp {
                rectangle "Port 80" as seqClusterIpPort80 {
                }
            }
            
            rectangle "Pod" as productServicePod {
                rectangle "Product Service Container" as productServiceContainer {
                }
                rectangle "Port 80" as productServicePort80 {
                }
            }

            rectangle "Cluster IP" as productServiceClusterIp {
                rectangle "Port 80" as productServiceClusterIpPort80 {
                }
            }
            
            rectangle "Pod" as productDatabasePod {
                rectangle "Product MSSQL Server Container" as productDatabaseContainer {
                }
                rectangle "Port 80" as productDatabasePort80 {
                }
            }

            rectangle "Cluster IP" as productDatabaseClusterIp {
                rectangle "Port 80" as productDatabaseClusterIpPort80 {
                }
            }
            
            rectangle "Pod" as orderServicePod {
                rectangle "Order Service Container" as orderServiceContainer {
                }
                rectangle "Port 80" as orderServicePort80 {
                }
            }

            rectangle "Cluster IP" as orderServiceClusterIp {
                rectangle "Port 80" as orderServiceClusterIpPort80 {
                }
            }
            
            rectangle "Pod" as orderDatabasePod {
                rectangle "Order MSSQL Server Container" as orderDatabaseContainer {
                }
                rectangle "Port 80" as orderDatabasePort80 {
                }
            }

            rectangle "Cluster IP" as orderDatabaseClusterIp {
                rectangle "Port 80" as orderDatabaseClusterIpPort80 {
                }
            }
            
            rectangle "Pod" as paymentServicePod {
                rectangle "Payment Service Container" as paymentServiceContainer {
                }
                rectangle "Port 80" as paymentServicePort80 {
                }
            }

            rectangle "Cluster IP" as paymentServiceClusterIp {
                rectangle "Port 80" as paymentServiceClusterIpPort80 {
                }
            }

            rectangle "Cluster IP" as orderServiceClusterIp {
                rectangle "Port 80" as orderServiceClusterIpPort80 {
                }
            }
            
            rectangle "Pod" as paymentDatabasePod {
                rectangle "Order MSSQL Server Container" as paymentDatabaseContainer {
                }
                rectangle "Port 80" as paymentDatabasePort80 {
                }
            }

            rectangle "Cluster IP" as paymentDatabaseClusterIp {
                rectangle "Port 80" as paymentDatabaseClusterIpPort80 {
                }
            }
            
            rectangle "Pod" as shoppingCartServicePod {
                rectangle "Shopping Cart Service Container" as shoppingCartServiceContainer {
                }
                rectangle "Port 80" as shoppingCartServicePort80 {
                }
            }

            rectangle "Cluster IP" as shoppingCartServiceClusterIp {
                rectangle "Port 80" as shoppingCartServiceClusterIpPort80 {
                }
            }
            
            rectangle "Pod" as shoppingCartDatabasePod {
                rectangle "Shopping Car MSSQL Server Container" as shoppingCartDatabaseContainer {
                }
                rectangle "Port 80" as shoppingCartDatabasePort80 {
                }
            }

            rectangle "Cluster IP" as shoppingCartDatabaseClusterIp {
                rectangle "Port 80" as shoppingCartDatabaseClusterIpPort80 {
                }
            }
        }
    }

    clients <-[hidden]down-> cluster

    ingressNginxLoadBalancerPort80 <-down-> clients
    ingressNginxLoadBalancerPort80 <-down-> ingressNginxPort80

    ingressNginxPod <-[hidden]down-> rabbitMqPod
    ingressNginxPod <-[hidden]down-> seqPod
    ingressNginxPod <-[hidden]down-> productDatabasePod
    ingressNginxPod <-[hidden]down-> orderDatabasePod
    ingressNginxPod <-[hidden]down-> paymentDatabasePod
    ingressNginxPod <-[hidden]down-> shoppingCartDatabasePod

    ingressNginxPort80 <-down-> productServicePort80
    ingressNginxPort80 <-down-> orderServicePort80
    ingressNginxPort80 <-down-> paymentServicePort80
    ingressNginxPort80 <-down-> shoppingCartServicePort80

    rabbitMqPod <-[hidden]right-> seqPod
    seqPod <-[hidden]right-> productServicePod
    productServicePod <-[hidden]right-> productDatabasePod
    productDatabasePod <-[hidden]right-> orderServicePod
    orderServicePod <-[hidden]right-> orderDatabasePod
    orderDatabasePod <-[hidden]right-> paymentServicePod
    paymentServicePod <-[hidden]right-> paymentDatabasePod
    paymentDatabasePod <-[hidden]right-> shoppingCartServicePod
    shoppingCartServicePod <-[hidden]right-> shoppingCartDatabasePod

    rabbitMqContainer <-down-> rabbitMqPort5672
    rabbitMqContainer <-down-> rabbitMqPort15672
    rabbitMqPort5672 <-down-> rabbitMqClusterIpPort5672
    rabbitMqPort15672 <-down-> rabbitMqClusterIpPort15672

    seqContainer <-down-> seqPort80
    seqPort80 <-down-> seqClusterIpPort80

    productServiceContainer <-down-> productServicePort80
    productServicePort80 <-down-> productServiceClusterIpPort80

    productDatabaseContainer <-down-> productDatabasePort80
    productDatabasePort80 <-down-> productDatabaseClusterIpPort80

    orderServiceContainer <-down-> orderServicePort80
    orderServicePort80 <-down-> orderServiceClusterIpPort80

    orderDatabaseContainer <-down-> orderDatabasePort80
    orderDatabasePort80 <-down-> orderDatabaseClusterIpPort80

    paymentServiceContainer <-down-> paymentServicePort80
    paymentServicePort80 <-down-> paymentServiceClusterIpPort80

    paymentDatabaseContainer <-down-> paymentDatabasePort80
    paymentDatabasePort80 <-down-> paymentDatabaseClusterIpPort80

    shoppingCartServiceContainer <-down-> shoppingCartServicePort80
    shoppingCartServicePort80 <-down-> shoppingCartServiceClusterIpPort80

    shoppingCartDatabaseContainer <-down-> shoppingCartDatabasePort80
    shoppingCartDatabasePort80 <-down-> shoppingCartDatabaseClusterIpPort80

    rabbitMqClusterIp <-left-> seqClusterIp
    seqClusterIp <-left-> productServiceClusterIp
    productServiceClusterIp <-left-> productDatabaseClusterIp
    productDatabaseClusterIp <-left-> orderServiceClusterIp
    orderServiceClusterIp <-left-> orderDatabaseClusterIp
    orderDatabaseClusterIp <-left-> paymentServiceClusterIp
    paymentServiceClusterIp <-left-> paymentDatabaseClusterIp
    paymentDatabaseClusterIp <-left-> shoppingCartServiceClusterIp
    shoppingCartServiceClusterIp <-left-> shoppingCartDatabaseClusterIp
@enduml
```