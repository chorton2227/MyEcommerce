import React, { useEffect, useState } from "react";
import {
  Configuration,
  ProductsApi,
  ProductReadDto,
} from "typescript-axios-product-service";
import ProgressBar from "react-bootstrap/ProgressBar";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Card from "react-bootstrap/Card";
import { Button } from "react-bootstrap";
import NextLink from "next/link";

const ProductList: React.FC<{}> = () => {
  const [page, setPage] = useState(0);
  const [limit] = useState(12);
  const [isLoading, setIsLoading] = useState(true);
  const [hasMore, setHasMore] = useState(false);
  const [products, setProducts] = useState<ProductReadDto[]>([]);

  useEffect(() => {
    getProducts();
  }, [page]);

  const getProducts = async () => {
    setIsLoading(true);
    const productsApiService = new ProductsApi(
      new Configuration({
        basePath: "https://localhost:6001",
      })
    );
    const response = await productsApiService.getAll(page, limit);
    setHasMore(response.data.hasMore ?? false);
    setProducts([...products, ...(response.data.products ?? [])]);
    setIsLoading(false);
  };

  return (
    <Container>
      <Row>
        {products.map((product: ProductReadDto, index: any) => (
          <Col key={product.id} lg={3} md={4} sm={6}>
            <Card className="mb-4">
              <Card.Img variant="top" src={product.imageUri!} />
              <Card.Body>
                <Card.Title>
                  <NextLink href="/product/[id]" as={`/product/${product.id}`}>
                    <a>{product.name}</a>
                  </NextLink>
                </Card.Title>
                <Card.Text>{product.description}</Card.Text>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
      {isLoading ? (
        <ProgressBar striped variant="info" now={100} />
      ) : (
        <Row>
          <Button
            variant="primary"
            disabled={!hasMore}
            onClick={() => setPage(page! + 1)}
          >
            Load More
          </Button>
        </Row>
      )}
    </Container>
  );
};

export default ProductList;
