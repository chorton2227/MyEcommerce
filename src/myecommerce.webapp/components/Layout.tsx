import React from "react";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Navigation from "./Navigation";

const Layout: React.FC<{}> = ({ children }) => {
  return (
    <>
      <Navigation />
      <Container>
        <Row>{children}</Row>
      </Container>
    </>
  );
};

export default Layout;
