import React, { Component } from "react";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import "./Login_register.css";
import { variables } from "../Variables";
import {
  MDBInput,
  MDBCol,
  MDBRow,
  MDBContainer,
  MDBCard,
  MDBCardBody,
  MDBCardTitle,
  MDBCardText,
  MDBBtn,
  MDBValidation,
  MDBValidationItem,
} from "mdb-react-ui-kit";

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

  myFunctionRegister() {
    var x = document.getElementById("loginPasswordRegister");
    if (x.type === "password") {
      x.type = "text";
    } else {
      x.type = "password";
    }
  }

  onKeyPress = (e) => {
    if (e.which === 13) {
      this.login();
    }
  };

  onKeyPressRegister = (e) => {
    if (e.which === 13) {
      this.register();
    }
  };

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
        console.log(localStorage.token);
        if (
          localStorage.token == "undefined" ||
          localStorage.token == "" ||
          localStorage.token == undefined
        ) {
          return;
        } else {
          window.location.assign("http://127.0.0.1:5173/");
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
    }).then((res) => res.json())
    .then(
      window.location.assign("http://127.0.0.1:5173/login_register")
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
        <MDBContainer className="px-4 mt-3">
          <MDBRow className="gx-5">
            <MDBCol className="p-5 mt-3">
              <MDBCard className="p-5 mt-5">
                <MDBValidation>
                  <MDBCardBody>
                    <MDBCardTitle className="mb-5">
                      Already have an account? Login
                    </MDBCardTitle>
                    <MDBCardText className="m-3 mb-4">
                      <MDBInput
                        label="Username"
                        id="typeText"
                        type="text"
                        value={LoginUsername}
                        onChange={this.changeLoginUsername}
                        required
                      />
                    </MDBCardText>
                    <MDBCardText className="m-3">
                      <MDBInput
                        label="Password"
                        id="loginPassword"
                        type="password"
                        value={LoginPassword}
                        onChange={this.changeLoginPassword}
                        onKeyPress={this.onKeyPress}
                        required
                      />
                    </MDBCardText>
                    <MDBCardText className="m-3">
                      <input
                        type="checkbox"
                        onClick={() => this.myFunction()}
                      />{" "}
                      Show password
                    </MDBCardText>
                    <MDBBtn
                      className="mt-2"
                      id="loginBtn"
                      color="success"
                      onClick={() => this.login()}
                      type="submit"
                    >
                      Login
                    </MDBBtn>
                  </MDBCardBody>
                </MDBValidation>
              </MDBCard>
            </MDBCol>
            <MDBCol>
              <MDBCard className="p-5 mt-3 mb-5">
                <MDBValidation>
                  <MDBCardTitle className="mb-5">
                    New here? Register
                  </MDBCardTitle>

                  <MDBRow className="mb-5">
                    <MDBCol>
                      <MDBInput
                        label="First name"
                        id="typeText"
                        type="text"
                        value={FirstName}
                        onChange={this.changeFirstName}
                        required
                      />
                    </MDBCol>
                    <MDBCol>
                      <MDBInput
                        label="Last name"
                        id="typeText"
                        type="text"
                        value={LastName}
                        onChange={this.changeLastName}
                        required
                      />
                    </MDBCol>
                  </MDBRow>

                  <MDBRow className="mb-5">
                    <MDBCol>
                      <MDBInput
                        label="Adress"
                        id="typeText"
                        type="text"
                        value={Adress}
                        onChange={this.changeAdress}
                        required
                      />
                    </MDBCol>
                    <MDBCol>
                      <MDBInput
                        label="City"
                        id="typeText"
                        type="text"
                        value={City}
                        onChange={this.changeCity}
                        required
                      />
                    </MDBCol>
                  </MDBRow>
                  <MDBRow className="mb-5">
                    <MDBCol>
                      <MDBInput
                        label="Email"
                        id="typeText"
                        type="text"
                        value={Email}
                        onChange={this.changeEmail}
                        required
                      />
                    </MDBCol>
                    <MDBCol>
                      <MDBInput
                        label="Phone"
                        id="typeText"
                        type="text"
                        value={Phone}
                        onChange={this.changePhone}
                        required
                      />
                    </MDBCol>
                  </MDBRow>

                  <MDBCardText className="m-4">
                    <MDBInput
                      label="Username"
                      id="typeText"
                      type="text"
                      value={Username}
                      onChange={this.changeUsername}
                      required
                    />
                  </MDBCardText>
                  <MDBCardText className="m-4">
                    <MDBInput
                      label="Password"
                      id="loginPasswordRegister"
                      type="password"
                      value={PwdHash}
                      onChange={this.changePwdHash}
                      onKeyPress={this.onKeyPressRegister}
                      required
                    />
                  </MDBCardText>
                  <MDBCardText className="m-4">
                    <input
                      type="checkbox"
                      onClick={() => this.myFunctionRegister()}
                    />{" "}
                    Show password
                  </MDBCardText>
                  <MDBBtn
                    type="submit"
                    className="mt-2"
                    id="loginBtn"
                    color="warning"
                    onClick={() => this.register()}
                  >
                    Register
                  </MDBBtn>
                </MDBValidation>
              </MDBCard>
            </MDBCol>
          </MDBRow>
        </MDBContainer>
      </div>
    );
  }
}
