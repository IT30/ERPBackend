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
import { About } from "./pages/About";
import { BrowserRouter, Route, Routes, Link } from "react-router-dom";
import jwt_decode from "jwt-decode";
import { MDBFooter } from "mdb-react-ui-kit";

function logout() {
  console.log(localStorage.token);
  localStorage.setItem("token", null);
  console.log(localStorage.token);
  window.location.reload(false);
  decodedRole = "VISITOR";
}
var token;
var decoded = "";
var decodedRole = "VISITOR";

try {
  console.log(decodedRole);
  token = localStorage.getItem("token");
  decoded = jwt_decode(token);
  if (
    decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] !=
    null
  ) {
    decodedRole =
      decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
  } else {
    decodedRole = "VISITOR";
  }

  console.log(
    decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
  );

  var decodedHeader = jwt_decode(token, { header: true });
  console.log(decodedHeader);
} catch (e) {}

function App() {
  /* useEffect(() => {
    var datum = new Date();
    var logindate = new Date(localStorage.time);
    if (logindate != null) {
      if (logindate - datum > 3600000) {
        localStorage.removeItem("token");
      } else {
      }
    }
  }); */

  return (
    <BrowserRouter>
      <div className="App container-fluid">
        {/* <img src="src\assets\images\MainBackground.png"/> */}
        <div
          className="bg-image row"
          style={{
            backgroundImage: "url(src/assets/images/MainBackgroundSmaller.png)",
            backgroundRepeat: "repeat-x",
            height: "150px",
          }}
        ></div>

        <Navbar
          className="row"
          collapseOnSelect
          expand="lg"
          bg="success"
          variant="dark"
          sticky="top"
        >
          <Container>
            <Navbar.Brand href="#home">
              FARMA STIG {decodedRole === "ADMIN" ? "ADMIN" : null}
            </Navbar.Brand>
            <Navbar.Toggle aria-controls="responsive-navbar-nav" />
            <Navbar.Collapse id="responsive-navbar-nav">
              <Nav className="mx-auto">
                <Nav.Link as={Link} to="/">
                  <button type="button" className="btn btn-warning">
                    Home
                  </button>
                </Nav.Link>

                {decodedRole === "ADMIN" ? (
                  <>
                    <Nav.Link as={Link} to="/product">
                      <button type="button" className="btn btn-warning">
                        Product
                      </button>
                    </Nav.Link>
                    <Nav.Link as={Link} to="/users">
                      <button type="button" className="btn btn-warning">
                        Users
                      </button>
                    </Nav.Link>
                    <Nav.Link as={Link} to="/orders">
                      <button type="button" className="btn btn-warning">
                        Orders
                      </button>
                    </Nav.Link>
                    <Nav.Link as={Link} to="/className">
                      <button type="button" className="btn btn-warning">
                        Classes
                      </button>
                    </Nav.Link>
                    <Nav.Link as={Link} to="/productType">
                      <button type="button" className="btn btn-warning">
                        Product Types
                      </button>
                    </Nav.Link>
                    <Nav.Link as={Link} to="/origin">
                      <button type="button" className="btn btn-warning">
                        Origins
                      </button>
                    </Nav.Link>
                  </>
                ) : null}
                <Nav.Link as={Link} to="/about">
                      <button type="button" className="btn btn-warning">
                        About
                      </button>
                    </Nav.Link>
              </Nav>
              <Nav>
                {decodedRole === "VISITOR" ? (
                  <Nav.Link as={Link} to="/login_register">
                    <button
                      type="button"
                      className="btn btn-success float-start"
                    >
                      Login/Register
                    </button>
                  </Nav.Link>
                ) : null}
                {decodedRole !== "VISITOR" ? (
                  <Nav.Link as={Link} to="/">
                    <button
                      type="button"
                      className="btn btn-danger float-start"
                      onClick={() => logout()}
                    >
                      Logout
                    </button>
                  </Nav.Link>
                ) : null}
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
          <Route path="/className" element={<Classe />} />
          <Route path="/productType" element={<ProductType />} />
          <Route path="/origin" element={<Origin />} />
          <Route path="/about" element={<About />} />
        </Routes>
      </div>

      <MDBFooter bgColor="success" className="text-center text-lg-left">
        <div
          className="text-center p-3 text-light"
          style={{ backgroundColor: "rgba(0, 0, 0, 0.2)" }}
        >
          &copy; {new Date().getFullYear()} Copyright: Aleksandar Peric
        </div>
      </MDBFooter>
    </BrowserRouter>
  );
}

export default App;
