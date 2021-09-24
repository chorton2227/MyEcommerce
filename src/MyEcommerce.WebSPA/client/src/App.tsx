import React, { useState, useEffect } from 'react';
import ProgressBar from 'react-bootstrap/ProgressBar';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { Configuration, ProductsApi, ProductReadDto } from 'typescript-axios-product-service';
import './App.css';

function App() {
  const [isLoading, setLoading] = useState(true);
  const [products, setProducts] = useState<any>({});

  useEffect(() => {
    getProducts();
  }, []);

  const getProducts = async () => {
    const productsApiService = new ProductsApi(new Configuration({ basePath: process.env.REACT_APP_PRODUCT_SERVICE_HOST }));
    const response = await productsApiService.getAll();
    setProducts(response.data);
    setLoading(false);
  };

  if (isLoading) {
    return (
      <div>
        <ProgressBar striped variant="info" now={100} />
      </div>
    )
  }

  return (
    <Container>
      <Row>
        {products.map((product: ProductReadDto, index: any) => (
          <Col key={index}>{product.name}</Col>
        ))}
      </Row>
    </Container>
  );
}

export default App;
