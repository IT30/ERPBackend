import React, { Component } from "react";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./Login_register.css";
import { variables } from "../Variables";
import Nav from "react-bootstrap/Nav";
import { useNavigate } from "react-router-dom";

export class Login_register extends Component {
  constructor(props) {
    super(props);

    this.state = {
      LoginUsername: "",
      LoginPassword: "",
      isOpen: false,

      IDUser: 0,
      Email: "",
      Username: "",
      FirstName: "",
      LastName: "",
      Adress: "",
      City: "",
      Phone: "",
      UserRole: "",
      ProfilePictureURL: "MainBackground.png",
      PhotoPath: variables.PHOTO_URL,
      PwdHash: "",
    };
  }

  myFunction() {
    var x = document.getElementById("loginPassword");
    if (x.type === "password") {
      x.type = "text";
    } else {
      x.type = "password";
    }
  }

  componentDidMount() {
    if (localStorage.getItem("token") != "undefined") {
    }
  }

  changeLoginUsername = (e) => {
    this.setState({ LoginUsername: e.target.value });
  };
  changeLoginPassword = (e) => {
    this.setState({ LoginPassword: e.target.value });
  };
  changeEmail = (e) => {
    this.setState({ Email: e.target.value });
  };
  changeUsername = (e) => {
    this.setState({ Username: e.target.value });
  };
  changeFirstName = (e) => {
    this.setState({ FirstName: e.target.value });
  };
  changeLastName = (e) => {
    this.setState({ LastName: e.target.value });
  };
  changeAdress = (e) => {
    this.setState({ Adress: e.target.value });
  };
  changeCity = (e) => {
    this.setState({ City: e.target.value });
  };
  changePhone = (e) => {
    this.setState({ Phone: e.target.value });
  };
  changeUserRole = (e) => {
    this.setState({ UserRole: e.target.value });
  };
  changePwdHash = (e) => {
    this.setState({ PwdHash: e.target.value });
  };

  login() {
    fetch(variables.API_URL + "auth", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        Username: this.state.LoginUsername,
        Password: this.state.LoginPassword,
      }),
    })
      .then((response) => response.json())
      .then((data) => {
        localStorage.setItem("token", data.token);
        var datum = new Date();
        localStorage.setItem("time", datum);
        console.log(localStorage.token);
        console.log(localStorage.time);
        if (
          localStorage.token == "undefined" ||
          localStorage.token == "" ||
          localStorage.token == undefined
        ) {
          alert("Failed");
          return;
        } else {
          console.log("yes");
          window.location.assign("http://127.0.0.1:5173/")
        }
      });
  }

  register() {
    fetch(variables.API_URL + "users", {
      method: "POST",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        ProfilePictureURL: this.state.ProfilePictureURL,
        Email: this.state.Email,
        Username: this.state.Username,
        FirstName: this.state.FirstName,
        LastName: this.state.LastName,
        Adress: this.state.Adress,
        City: this.state.City,
        Phone: this.state.Phone,
        PwdHash: this.state.PwdHash,
      }),
    })
      .then((res) => res.json())
      .then(
        (result) => {
          alert(result);
        },
        (error) => {
          alert("Failed");
        }
      );
  }

  render() {
    const {
      LoginUsername,
      LoginPassword,
      IDUser,
      Email,
      Username,
      FirstName,
      LastName,
      Adress,
      City,
      Phone,
      UserRole,
      PhotoPath,
      ProfilePictureURL,
      PwdHash,
    } = this.state;
    return (
      <div>
        <Container className="mt-5">
          <Row>
            <Col>
              <section className="h-70 bg-dark">
                <div className="container py-1 h-100">
                  <div className="row d-flex justify-content-center align-items-top h-100">
                    <div className="col">
                      <div className="card card-registration my-4">
                        <div className="card-body p-md-5 text-black">
                          <h3 className="mb-5">
                            Already have an account? Login
                          </h3>
                          <div className="form-outline mb-4">
                            <input
                              type="text"
                              id="loginUsername"
                              className="form-control form-control-lg"
                              value={LoginUsername}
                              onChange={this.changeLoginUsername}
                            />
                            <label
                              className="form-label"
                              htmlFor="form3Example97"
                            >
                              Username
                            </label>
                          </div>
                          <div className="form-outline mb-4">
                            <input
                              type="password"
                              id="loginPassword"
                              className="form-control form-control-lg"
                              value={LoginPassword}
                              onChange={this.changeLoginPassword}
                            />
                            <label
                              className="form-label"
                              htmlFor="form3Example97"
                            >
                              Password
                            </label>
                          </div>
                          <input
                            type="checkbox"
                            onClick={() => this.myFunction()}
                          />
                          Show Password
                          <div className="d-flex justify-content-end pt-3">
                            <button
                              type="button"
                              /* data-bs-toggle="modal"
                              data-bs-target="#staticBackdrop" */
                              className="btn btn-warning btn-lg ms-2"
                              onClick={() => this.login()}
                            >
                              Login
                            </button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </section>
            </Col>
            <Col>
              <section className="h-70 bg-dark">
                <div className="container py-1 h-100">
                  <div className="row d-flex justify-content-center align-items-center h-100">
                    <div className="col">
                      <div className="card card-registration my-4">
                        <div className="card-body p-md-5 text-black">
                          <h3 className="mb-5">New here? Register</h3>

                          <div className="row">
                            <div className="col-md-6 mb-4">
                              <div className="form-outline">
                                <input
                                  type="text"
                                  id="firstName"
                                  className="form-control form-control-lg"
                                  value={FirstName}
                                  onChange={this.changeFirstName}
                                />
                                <label
                                  className="form-label"
                                  htmlFor="form3Example1m"
                                >
                                  First name
                                </label>
                              </div>
                            </div>
                            <div className="col-md-6 mb-4">
                              <div className="form-outline">
                                <input
                                  type="text"
                                  id="lastName"
                                  className="form-control form-control-lg"
                                  value={LastName}
                                  onChange={this.changeLastName}
                                />
                                <label
                                  className="form-label"
                                  htmlFor="form3Example1n"
                                >
                                  Last name
                                </label>
                              </div>
                            </div>
                          </div>

                          <div className="row">
                            <div className="col-md-6 mb-4">
                              <div className="form-outline">
                                <input
                                  type="text"
                                  id="adress"
                                  className="form-control form-control-lg"
                                  value={Adress}
                                  onChange={this.changeAdress}
                                />
                                <label
                                  className="form-label"
                                  htmlFor="form3Example1m"
                                >
                                  Adress
                                </label>
                              </div>
                            </div>
                            <div className="col-md-6 mb-4">
                              <div className="form-outline">
                                <input
                                  type="text"
                                  id="city"
                                  className="form-control form-control-lg"
                                  value={City}
                                  onChange={this.changeCity}
                                />
                                <label
                                  className="form-label"
                                  htmlFor="form3Example1n"
                                >
                                  City
                                </label>
                              </div>
                            </div>
                          </div>
                          <div className="row">
                            <div className="col-md-6 mb-4">
                              <div className="form-outline">
                                <input
                                  type="text"
                                  id="phone"
                                  className="form-control form-control-lg"
                                  value={Phone}
                                  onChange={this.changePhone}
                                />
                                <label
                                  className="form-label"
                                  htmlFor="form3Example1m"
                                >
                                  Phone
                                </label>
                              </div>
                            </div>
                            <div className="col-md-6 mb-4">
                              <div className="form-outline">
                                <input
                                  type="text"
                                  id="username"
                                  className="form-control form-control-lg"
                                  value={Username}
                                  onChange={this.changeUsername}
                                />
                                <label
                                  className="form-label"
                                  htmlFor="form3Example1n"
                                >
                                  Username
                                </label>
                              </div>
                            </div>
                          </div>

                          <div className="form-outline mb-4">
                            <input
                              type="text"
                              id="email"
                              className="form-control form-control-lg"
                              value={Email}
                              onChange={this.changeEmail}
                            />
                            <label
                              className="form-label"
                              htmlFor="form3Example90"
                            >
                              Email
                            </label>
                          </div>

                          <div className="form-outline mb-4">
                            <input
                              type="text"
                              id="password"
                              className="form-control form-control-lg"
                              value={PwdHash}
                              onChange={this.changePwdHash}
                            />
                            <label
                              className="form-label"
                              htmlFor="form3Example97"
                            >
                              Password
                            </label>
                          </div>

                          <div className="d-flex justify-content-end pt-3">
                            <button
                              type="button"
                              className="btn btn-warning btn-lg ms-2"
                              onClick={() => this.register()}
                            >
                              Register
                            </button>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </section>
            </Col>
          </Row>
        </Container>

        {/* <div
          className="modal fade"
          id="staticBackdrop"
          data-bs-backdrop="static"
          data-bs-keyboard="false"
          tabIndex="-1"
          aria-labelledby="staticBackdropLabel"
          aria-hidden="true"
        >
          <div className="modal-dialog">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title" id="staticBackdropLabel">
                  Notification
                </h5>
              </div>
              {localStorage.getItem("token") != "undefined" ? (
                <>
                  <div className="modal-body">Login successful</div>
                  <div className="modal-footer">
                    <Nav.Link href="/">
                      <button type="button" className="btn btn-primary">
                        Cool
                      </button>
                    </Nav.Link>
                  </div>
                </>
              ) : null}

              {localStorage.getItem("token") == "undefined" ? (
                <>
                  <div className="modal-body">Login failed</div>
                  <div className="modal-footer">
                  <Nav.Link href="/login_register">
                      <button type="button" className="btn btn-primary">
                        Damn
                      </button>
                    </Nav.Link>
                  </div>
                </>
              ) : null}
            </div>
          </div>
        </div> */}
      </div>
    );
  }
}
