import React, { Component } from "react";
import { variables } from "../Variables";
import { useNavigate } from "react-router-dom";
import {
  MDBCard,
  MDBCardImage,
  MDBCardBody,
  MDBCardTitle,
  MDBCardText,
  MDBCardLink,
  MDBListGroup,
  MDBListGroupItem,
  MDBCardHeader,
  MDBBtn,
  MDBRow,
  MDBCol,
  MDBRange,
} from "mdb-react-ui-kit";

var pathArray = window.location.pathname.split("/");
var id = pathArray[2];

export class Cart extends Component {
  constructor(props) {
    super(props);

    this.state = {
      users: "",
      cart: [],
      products: [],
      modalTitle: "",
      IDUser: 0,
      Email: "",
      Username: "",
      FirstName: "",
      LastName: "",
      Adress: "",
      City: "",
      Phone: "",
      UserRole: "",
      ProfilePictureURL: "default.jpg",
      PhotoPath: variables.PHOTO_URL,
      PwdHash: "",
      Empty: "",
      ProductPictureURL: "",

      IDCartItem: 0,
      IDProduct: "",
      ProductName: "",
      ProductAmount: "",
      CartAmount: "",
      CartPrice: "",
    };
  }

  handleRangeChange = (event) => {
    this.setState({ CartAmount: event.target.value });
  };

  refreshList(id) {
    console.log(id);
    this.setState({ IDUser: id });
    fetch(variables.API_URL + "users/" + id, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then((response) => {
        if (response.status == 403) {
          useNavigate("/home");
        }
        return response.json();
      })
      .then((data) => {
        console.log(data);
        this.setState({ users: data });
      });
    fetch(variables.API_URL + "cart/user/" + id, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then((response) => {
        this.setState({ IDUser: id });
        return response.json();
      })
      .then((data) => {
        if (data != null) {
          console.log(data);
          this.setState({ cart: data });
        } else {
          this.setState({ cart: null });
        }
      })
      .catch((error) => {
        if (
          error.name === "SyntaxError" &&
          error.message.includes("Unexpected end of JSON input")
        ) {
          console.error("User has no items in the cart");
          this.setState({ cart: [] });
          this.setState({ Empty: "is empty" });
        } else {
          console.error(error);
        }
      });
    fetch(variables.API_URL + "product")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        this.setState({ products: data });
      });
  }

  refreshCart(id, name) {
    fetch(variables.API_URL + "cart/user/" + id, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then((response) => {
        this.setState({ FirstName: name });
        this.setState({ IDUser: id });
        return response.json();
      })
      .then((data) => {
        if (data != null) {
          console.log(data);
          this.setState({ cart: data });
        } else {
          this.setState({ cart: null });
        }
      })
      .catch((error) => {
        if (
          error.name === "SyntaxError" &&
          error.message.includes("Unexpected end of JSON input")
        ) {
          console.error("User has no items in the cart");
          this.setState({ cart: [] });
          this.setState({ Empty: "is empty" });
        } else {
          console.error(error);
        }
      });
    fetch(variables.API_URL + "product")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        this.setState({ products: data });
      });
  }

  componentDidMount() {
    var pathArray = window.location.pathname.split("/");
    var id = pathArray[2];
    console.log(id);
    this.refreshList(id);
  }

  openCart(id, name) {
    if (this.state.WhichTab == 0) {
      this.refreshCart(id, name);
      this.setState({ WhichTab: 1 });
    } else {
      this.setState({ WhichTab: 0 });
      this.setState({ Empty: "" });
    }
  }

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

  changeCartUser = (e) => {
    this.setState({ IDUser: e.target.value });
  };
  changeCartProduct = (e) => {
    this.setState({ ProductName: e.target.value });
    this.setState({
      IDProduct:
        e.target.options[e.target.options.selectedIndex].getAttribute(
          "data-key"
        ),
    });
    this.setState({
      ProductAmount:
        e.target.options[e.target.options.selectedIndex].getAttribute(
          "data-amount"
        ),
    });
    console.log(this.state.ProductAmount);
  };
  changeCartAmount = (e) => {
    this.setState({ CartAmount: e.target.value });
  };
  changeCartPrice = (e) => {
    this.setState({ CartPrice: e.target.value });
  };

  addCartClick() {
    this.setState({
      modalTitle: "Add cart item",
      IDCartItem: 0,
      IDUser: this.state.IDUser,
      IDProduct: "",
      CartAmount: "",
      CartPrice: "",
    });
  }
  editClick(user) {
    this.setState({
      modalTitle: "Edit User",
      IDUser: user.idUser,
      Email: user.email,
      Username: user.username,
      FirstName: user.firstName,
      LastName: user.lastName,
      Adress: user.adress,
      City: user.city,
      Phone: user.phone,
      UserRole: user.userRole,
      ProfilePictureURL: user.profilePictureURL,
      PwdHash: "",
    });
  }

  editCartClick(cart) {
    this.setState({
      modalTitle: "Update cart item",
      IDCartItem: cart.idCartItem,
      IDUser: cart.idUser,
      IDProduct: cart.idProduct,
      CartAmount: cart.cartAmount,
      CartPrice: cart.cartPrice,
    });
  }

  createCartClick() {
    fetch(variables.API_URL + "cart", {
      method: "POST",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDUser: this.state.IDUser,
        IDProduct: this.state.IDProduct,
        CartAmount: this.state.CartAmount,
        CartPrice: this.state.CartPrice,
      }),
    })
      .then((res) => "Created successfuly")
      .then(
        (result) => {
          alert(result);
          this.refreshList();
        },
        (error) => {
          alert("Failed");
        }
      );
  }

  updateClick() {
    fetch(variables.API_URL + "users", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDUser: this.state.IDUser,
        Email: this.state.Email,
        Username: this.state.Username,
        FirstName: this.state.FirstName,
        LastName: this.state.LastName,
        Adress: this.state.Adress,
        City: this.state.City,
        Phone: this.state.Phone,
        UserRole: this.state.UserRole,
        ProfilePictureURL: this.state.ProfilePictureURL,
        PwdHash: this.state.PwdHash,
      }),
    })
      .then((res) => "Updated successfuly")
      .then(
        (result) => {
          alert(result);
          this.refreshList(IDUser);
        },
        (error) => {
          alert("Failed");
        }
      );
  }

  updateCartClick() {
    fetch(variables.API_URL + "cart", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDCartItem: this.state.IDCartItem,
        IDUser: this.state.IDUser,
        IDProduct: this.state.IDProduct,
        CartAmount: this.state.CartAmount,
        CartPrice: this.state.CartPrice,
      }),
    })
      .then((res) => "Updated successfuly")
      .then(
        (result) => {
          alert(result);
          this.refreshList();
        },
        (error) => {
          alert("Failed");
        }
      );
  }

  deleteClick(id) {
    if (window.confirm("Are you sure?")) {
      fetch(variables.API_URL + "users/" + id, {
        method: "DELETE",
        headers: {
          Accept: "application/json",
          Authorization: "Bearer " + localStorage.getItem("token"),
          "Content-Type": "application/json",
        },
      })
        .then((res) => "Deleted successfuly")
        .then(
          (result) => {
            alert(result);
            localStorage.setItem("token", null);
            window.location.assign("http://127.0.0.1:5173/");
          },
          (error) => {
            alert("Failed");
          }
        );
    }
  }

  deleteCartClick(id) {
    if (window.confirm("Are you sure?")) {
      fetch(variables.API_URL + "cart/" + id, {
        method: "DELETE",
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json",
        },
      })
        .then((res) => "Deleted successfuly")
        .then(this.refreshList(this.state.IDUser), window.location.reload());
    }
  }

  imageUpload = (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("file", e.target.files[0], e.target.files[0].name);

    fetch(variables.API_URL + "users/photo", {
      method: "POST",
      body: formData,
    })
      .then((res) => res.json())
      .then((data) => {
        this.setState({ ProfilePictureURL: data });
      });
  };

  render() {
    const {
      users,
      products,
      modalTitle,
      WhichTab,
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
      ProductPictureURL,
      PwdHash,
      Empty,

      cart,
      IDCartItem,
      IDProduct,
      ProductName,
      ProductAmount,
      CartAmount,
      CartPrice,
    } = this.state;

    return (
      <div className="mt-3 mb-3">
        <MDBRow>
          <MDBCol md="9">
            <MDBRow>
              <MDBCol md="6">
                <MDBCard>
                  {cart.map((car) => (
                    <MDBCard className="mb-3" key={car.idCartItem}>
                      <MDBRow className="g-0">
                        <MDBCol md="2">
                          {products.map((pr) => {
                            if (pr.idProduct == car.idProduct) {
                              return (
                                <MDBCardImage
                                  src={PhotoPath + pr.productPictureURL}
                                  alt="..."
                                  className="w-100"
                                />
                              );
                            }
                          })}
                        </MDBCol>
                        <MDBCol md="8">
                          <MDBCardBody>
                            {products.map((pr) => {
                              if (pr.idProduct == car.idProduct) {
                                return (
                                  <MDBCardTitle>{pr.productName}</MDBCardTitle>
                                );
                              }
                            })}
                            <MDBCardText>{car.cartAmount}</MDBCardText>
                            <MDBCardText>
                              {products.map((pr) => {
                                if (pr.idProduct == car.idProduct) {
                                  return (
                                    <MDBRange
                                      defaultValue={car.cartAmount}
                                      value={this.CartAmount}
                                      min="1"
                                      max={pr.supplyKG}
                                      step="1"
                                      id="customRange3"
                                      label="Kolicina(*100g/komada)"
                                      onChange={this.handleRangeChange}
                                    />
                                  );
                                }
                              })}
                            </MDBCardText>
                          </MDBCardBody>
                        </MDBCol>
                      </MDBRow>
                      <MDBBtn
                        className="me-1"
                        color="danger"
                        onClick={() => this.deleteCartClick(car.idCartItem)}
                      >
                        Delete item
                      </MDBBtn>
                    </MDBCard>
                  ))}
                </MDBCard>
              </MDBCol>
              <MDBCol md="6">
                <MDBCard></MDBCard>
              </MDBCol>
            </MDBRow>
          </MDBCol>
          <MDBCol md="3">
            <MDBCard>
              <MDBCardImage
                position="top"
                alt="..."
                src={PhotoPath + this.state.users.profilePictureURL}
              />
              <MDBCardBody>
                <MDBCardTitle>
                  {this.state.users.firstName} {this.state.users.lastName}
                </MDBCardTitle>
                <MDBCardText>
                  {this.state.users.username}
                  <br></br>
                  {this.state.users.email}
                </MDBCardText>
              </MDBCardBody>
              <MDBListGroup>
                <MDBListGroupItem>{this.state.users.adress}</MDBListGroupItem>
                <MDBListGroupItem>{this.state.users.city}</MDBListGroupItem>
                <MDBListGroupItem>{this.state.users.phone}</MDBListGroupItem>
              </MDBListGroup>
              <MDBCardBody>
                <MDBCardLink href="#">
                  <MDBBtn
                    data-bs-toggle="modal"
                    data-bs-target="#exampleModal"
                    onClick={() => this.editClick(this.state.users)}
                    className="me-1"
                    color="warning"
                  >
                    Update account
                  </MDBBtn>
                </MDBCardLink>
                <MDBCardLink href="#">
                  <MDBBtn
                    className="me-1"
                    color="danger"
                    onClick={() => this.deleteClick(this.state.users.idUser)}
                  >
                    Delete account
                  </MDBBtn>
                </MDBCardLink>
              </MDBCardBody>
            </MDBCard>
          </MDBCol>
        </MDBRow>

        {/* Users modal */}
        <div
          className="modal fade"
          id="exampleModal"
          tabIndex="-1"
          aria-hidden="true"
        >
          <div className="modal-dialog modal-lg modal-dialog-centered">
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">{modalTitle}</h5>
                <button
                  type="button"
                  className="btn-close"
                  data-bs-dismiss="modal"
                  aria-label="Close"
                ></button>
              </div>

              <div className="modal-body">
                <div className="d-flex flex-row bd-highlight mb-3">
                  <div className="p-2 w-50 bd-highlight">
                    <div className="input-group mb-3">
                      <span className="input-group-text">Email</span>
                      <input
                        type="text"
                        className="form-control"
                        value={Email}
                        onChange={this.changeEmail}
                      />
                    </div>

                    <div className="input-group mb-3">
                      <span className="input-group-text">Username</span>
                      <input
                        type="text"
                        className="form-control"
                        value={Username}
                        onChange={this.changeUsername}
                      />
                    </div>

                    <div className="input-group mb-3">
                      <span className="input-group-text">First name</span>
                      <input
                        type="text"
                        className="form-control"
                        value={FirstName}
                        onChange={this.changeFirstName}
                      />
                    </div>

                    <div className="input-group mb-3">
                      <span className="input-group-text">Last name</span>
                      <input
                        type="text"
                        className="form-control"
                        value={LastName}
                        onChange={this.changeLastName}
                      />
                    </div>

                    <div className="input-group mb-3">
                      <span className="input-group-text">Adress</span>
                      <input
                        type="text"
                        className="form-control"
                        value={Adress}
                        onChange={this.changeAdress}
                      />
                    </div>

                    <div className="input-group mb-3">
                      <span className="input-group-text">City</span>
                      <input
                        type="text"
                        className="form-control"
                        value={City}
                        onChange={this.changeCity}
                      />
                    </div>

                    <div className="input-group mb-3">
                      <span className="input-group-text">Phone</span>
                      <input
                        type="text"
                        className="form-control"
                        value={Phone}
                        onChange={this.changePhone}
                      />
                    </div>

                    <div className="input-group mb-3">
                      <span className="input-group-text">Password</span>
                      <input
                        type="text"
                        className="form-control"
                        value={PwdHash}
                        onChange={this.changePwdHash}
                      />
                    </div>
                  </div>
                  <div className="p-2 w-50 bd-highlight">
                    <img
                      width="250px"
                      height="250px"
                      src={PhotoPath + ProfilePictureURL}
                    />
                    <input
                      className="m-2"
                      type="file"
                      onChange={this.imageUpload}
                    />
                  </div>
                </div>

                <button
                  type="button"
                  className="btn btn-primary float-start"
                  onClick={() => this.updateClick()}
                >
                  Update
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
