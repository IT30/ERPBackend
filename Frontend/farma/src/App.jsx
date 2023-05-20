import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import "./App.css";
import { Home } from "./pages/Home";
import { Product } from "./pages/Product";
import { Users } from "./pages/Users";
import { Login_register } from "./pages/Login_register";
import { Orders } from "./pages/Orders";
import { Classe } from "./pages/Class";
import { ProductType } from "./pages/ProductType";
import { Origin } from "./pages/Origin";
import { BrowserRouter, Route, Routes, NavLink } from "react-router-dom";
import jwt_decode from "jwt-decode";


var token = localStorage.getItem("token");
var decoded = jwt_decode(token);

console.log(decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]);

var decodedHeader = jwt_decode(token, { header: true });
console.log(decodedHeader);

function App() {
  return (
    <BrowserRouter>
      <div className="App container-fluid">
        <h3 className="d-flex justify-content-center m-3">React JS Frontend</h3>

        <Navbar collapseOnSelect expand="lg" bg="light" variant="light" sticky="top">
          <Container>
            <Navbar.Brand href="#home">STIG ADMIN</Navbar.Brand>
            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
            <Navbar.Collapse id="responsive-navbar-nav">
              <Nav className="mx-auto">
                <Nav.Link href="/">Home</Nav.Link>
                <Nav.Link href="/product">Product</Nav.Link>
                <Nav.Link href="/users">Users</Nav.Link>
                <Nav.Link href="/orders">Orders</Nav.Link>
                <Nav.Link href="/class">Classes</Nav.Link>
                <Nav.Link href="/productType">ProductTypes</Nav.Link>
                <Nav.Link href="/origin">Origins</Nav.Link>

              </Nav>
              <Nav>
                <Nav.Link href="/login_register">Login/Register</Nav.Link>
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>

        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/product" element={<Product />} />
          <Route path="/login_register" element={<Login_register />} />
          <Route path="/users" element={<Users />} />
          <Route path="/orders" element={<Orders />} />
          <Route path="/class" element={<Classe />} />
          <Route path="/productType" element={<ProductType  />} />
          <Route path="/origin" element={<Origin  />} />

        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
