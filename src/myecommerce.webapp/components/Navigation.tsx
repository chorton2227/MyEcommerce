import React from "react";
import Navbar from "react-bootstrap/Navbar";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import NextLink from "next/link";

const Navigation: React.FC<{}> = () => {
  return (
    <Navbar>
      <Container>
        <Navbar.Brand>
          <NextLink href="/">
            <a>MyEcommerce</a>
          </NextLink>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="navigation" />
        <Navbar.Collapse id="navigation">
          <Nav>
            <Nav.Link>Home</Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default Navigation;
