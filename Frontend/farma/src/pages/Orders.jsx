import React, { Component } from "react";
import { variables } from "../Variables";
import { Product } from "./Product";

export class Orders extends Component {
  constructor(props) {
    super(props);

    this.state = {
      orders: [],
      orderItems: [],
      users: [],
      products: [],
      modalTitle: "",
      WhichTab: 0,
      IDOrder: 0,
      IDUser: "",
      TotalOrderPrice: "",
      TransactionDate: "",

      IDOrderItem: 0,
      IDProduct: "",
      OrderAmount: "",
      OrderPrice: "",
      ProductName: "",
      ProductAmount: "",

      FirstName: "",
      LastName: "",

      IDOrderFilter: "",
      OrderNameFilter: "",
      ordersWithoutFilter: [],
    };
  }

  FilterFn() {
    var IDOrderFilter = this.state.IDOrderFilter;
    var OrderNameFilter = this.state.OrderNameFilter;

    var filteredData = this.state.ordersWithoutFilter.filter(function (el) {
      return (
        el.IDOrder.toString()
          .toLowerCase()
          .includes(IDOrderFilter.toString().trim().toLowerCase()) &&
        el.orderName
          .toString()
          .toLowerCase()
          .includes(OrderNameFilter.toString().trim().toLowerCase())
      );
    });

    this.setState({ orders: filteredData });
  }

  sortResult(prop, asc) {
    var sortedData = this.state.ordersWithoutFilter.sort(function (a, b) {
      if (asc) {
        return a[prop] > b[prop] ? 1 : a[prop] < b[prop] ? -1 : 0;
      } else {
        return b[prop] > a[prop] ? 1 : b[prop] < a[prop] ? -1 : 0;
      }
    });

    this.setState({ orders: sortedData });
  }

  changeIDOrderFilter = (e) => {
    this.state.IDOrderFilter = e.target.value;
    this.FilterFn();
  };
  changeOrderNameFilter = (e) => {
    this.state.OrderNameFilter = e.target.value;
    this.FilterFn();
  };

  refreshList() {
    fetch(variables.API_URL + "orders")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        this.setState({ orders: data, ordersWithoutFilter: data });
      });
    fetch(variables.API_URL + "users", {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        this.setState({ users: data });
      });
  }

  refreshOrder(id) {
    fetch(variables.API_URL + "orderItem/order/" + id, {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then((response) => {
        this.setState({ IDOrder: id });
        console.log(response);
        if (response.body == null) console.log("Prazno");
        return response.json();
      })
      .then((data) => {
        if (data == null) console.log("Prazno");
        console.log(data);
        this.setState({ orderItems: data });
      })
      .catch((error) => {
        if (
          error.name === "SyntaxError" &&
          error.message.includes("Unexpected end of JSON input")
        ) {
          console.error("Order has no items in it");
          this.setState({ orderItems: [] });
        } else {
          console.error(error);
        }
      });
    fetch(variables.API_URL + "product")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        this.setState({ products: data, productsWithoutFilter: data });
      });
  }

  componentDidMount() {
    this.refreshList();
  }

  openOrder(id) {
    if (this.state.WhichTab == 0) {
      this.refreshOrder(id);
      this.setState({ WhichTab: 1 });
    } else {
      this.setState({ WhichTab: 0 });
    }
  }

  changeIDUser = (e) => {
    this.setState({ FirstName: e.target.value });
    this.setState({
      IDUser:
        e.target.options[e.target.options.selectedIndex].getAttribute(
          "data-key"
        ),
    });
  };

  changeTotalOrderPrice = (e) => {
    this.setState({ TotalOrderPrice: e.target.value });
  };

  changeTransactionDate = (e) => {
    this.setState({ TransactionDate: e.target.value });
  };

  changeIDOrder = (e) => {
    this.setState({ IDOrder: e.target.value });
  };

  changeOrderProduct = (e) => {
    this.setState({ ProductName: e.target.value });
    this.setState({
      IDProduct:
        e.target.options[e.target.options.selectedIndex].getAttribute(
          "data-key"
        ),
    });
    this.setState({ ProductAmount: e.target.options[e.target.options.selectedIndex].getAttribute(
      "data-amount"
    ), });
    console.log(this.state.ProductAmount);
    console.log(this.state.ProductAmount);
  };

  changeOrderAmount = (e) => {
    this.setState({ OrderAmount: e.target.value })
  };

  changeOrderPrice = (e) => {
    this.setState({ OrderPrice: e.target.value });
  };

  addClick() {
    this.setState({
      modalTitle: "Add Order",
      IDUser: "",
      TotalOrderPrice: "",
      TransactionDate: "",
      FirstName: "",
      LastName: "",
    });
  }

  addOrderClick() {
    this.setState({
      modalTitle: "Add order item",
      IDOrderItem: 0,
      IDOrder: this.state.IDOrder,
      IDProduct: "",
      ProductName: "",
      OrderAmount: "",
      OrderPrice: "",
    });
  }

  editClick(ord) {
    this.setState({
      modalTitle: "Edit Order",
      IDOrder: ord.idOrder,
      IDUser: ord.idUser,
      TotalOrderPrice: ord.totalOrderPrice,
      TransactionDate: ord.transactionDate,
    });
    console.log(ord.idUser);
  }

  editOrderClick(ordit) {
    this.setState({
      modalTitle: "Update order item",
      IDOrderItem: ordit.idOrderItem,
      IDOrder: ordit.idOrder,
      IDProduct: ordit.idProduct,
      OrderAmount: ordit.orderAmount,
      OrderPrice: ordit.orderPrice,
    });
  }

  createClick() {
    fetch(variables.API_URL + "orders", {
      method: "POST",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDUser: this.state.IDUser,
        TotalOrderPrice: this.state.TotalOrderPrice,
        TransactionDate: this.state.TransactionDate,
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

  createOrderClick() {
    console.log(this.state.ProductAmount);
    fetch(variables.API_URL + "orderItem", {
      method: "POST",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDOrder: this.state.IDOrder,
        IDProduct: this.state.IDProduct,
        OrderAmount: this.state.OrderAmount,
        OrderPrice: this.state.OrderPrice,
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
    console.log(this.state.IDUser);
    fetch(variables.API_URL + "orders", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDOrder: this.state.IDOrder,
        IDUser: this.state.IDUser,
        TotalOrderPrice: this.state.TotalOrderPrice,
        TransactionDate: this.state.TransactionDate,
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

  updateOrderClick() {
    fetch(variables.API_URL + "orderItem", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDOrderItem: this.state.IDOrderItem,
        IDOrder: this.state.IDOrder,
        IDProduct: this.state.IDProduct,
        OrderAmount: this.state.OrderAmount,
        OrderPrice: this.state.OrderPrice,
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
      fetch(variables.API_URL + "orders/" + id, {
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
            this.refreshList();
          },
          (error) => {
            alert("Failed");
          }
        );
    }
  }

  deleteOrderClick(id) {
    if (window.confirm("Are you sure?")) {
      fetch(variables.API_URL + "orderItem/" + id, {
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
            this.refreshList();
          },
          (error) => {
            alert("Failed");
          }
        );
    }
  }

  render() {
    const {
      orders,
      orderItems,
      users,
      products,
      modalTitle,
      WhichTab,
      IDOrder,
      IDUser,
      TotalOrderPrice,
      TransactionDate,
      ProductAmount,

      FirstName,
      LastName,
      ProductName,

      IDOrderItem,
      IDProduct,
      OrderAmount,
      OrderPrice,
    } = this.state;

    return (
      <div>
        <div className={WhichTab == 1 ? "d-flex justify-content-between" : ""}>
          {WhichTab == 1 ? (
            <>
              <button
                type="button"
                className="btn-secondary m-2 float-start"
                onClick={() => this.openOrder()}
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="20"
                  height="20"
                  fill="currentColor"
                  className="bi bi-box-arrow-left"
                  viewBox="0 0 16 16"
                >
                  <path
                    fillRule="evenodd"
                    d="M6 12.5a.5.5 0 0 0 .5.5h8a.5.5 0 0 0 .5-.5v-9a.5.5 0 0 0-.5-.5h-8a.5.5 0 0 0-.5.5v2a.5.5 0 0 1-1 0v-2A1.5 1.5 0 0 1 6.5 2h8A1.5 1.5 0 0 1 16 3.5v9a1.5 1.5 0 0 1-1.5 1.5h-8A1.5 1.5 0 0 1 5 12.5v-2a.5.5 0 0 1 1 0v2z"
                  />
                  <path
                    fillRule="evenodd"
                    d="M.146 8.354a.5.5 0 0 1 0-.708l3-3a.5.5 0 1 1 .708.708L1.707 7.5H10.5a.5.5 0 0 1 0 1H1.707l2.147 2.146a.5.5 0 0 1-.708.708l-3-3z"
                  />
                </svg>
              </button>

              <div className="h2">Order: {this.state.IDOrder}</div>
              <button
                type="button"
                className="btn btn-primary m-2 float-end"
                data-bs-toggle="modal"
                data-bs-target="#orderModal"
                onClick={() => this.addOrderClick()}
              >
                Add order item
              </button>
            </>
          ) : null}

          {WhichTab == 0 ? (
            <button
              type="button"
              className="btn btn-primary m-2 float-end"
              data-bs-toggle="modal"
              data-bs-target="#exampleModal"
              onClick={() => this.addClick()}
            >
              Add Order
            </button>
          ) : null}
        </div>

        {/* Orders table */}
        {WhichTab == 0 ? (
          <table className="table table-striped">
            <thead>
              <tr>
                <th>
                  <div className="d-flex flex-row">
                    <input
                      className="form-control m-2"
                      onChange={this.changeIDOrderFilter}
                      placeholder="Filter"
                    />

                    <button
                      type="button"
                      className="btn btn-light"
                      onClick={() => this.sortResult("IDOrder", true)}
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-arrow-down-square-fill"
                        viewBox="0 0 16 16"
                      >
                        <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                      </svg>
                    </button>

                    <button
                      type="button"
                      className="btn btn-light"
                      onClick={() => this.sortResult("IDOrder", false)}
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-arrow-up-square-fill"
                        viewBox="0 0 16 16"
                      >
                        <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                      </svg>
                    </button>
                  </div>
                  IDOrder
                </th>
                <th>
                  <div className="d-flex flex-row">
                    <input
                      className="form-control m-2"
                      onChange={this.changeOrderNameFilter}
                      placeholder="Filter"
                    />

                    <button
                      type="button"
                      className="btn btn-light"
                      onClick={() => this.sortResult("OrderName", true)}
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-arrow-down-square-fill"
                        viewBox="0 0 16 16"
                      >
                        <path d="M2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2zm6.5 4.5v5.793l2.146-2.147a.5.5 0 0 1 .708.708l-3 3a.5.5 0 0 1-.708 0l-3-3a.5.5 0 1 1 .708-.708L7.5 10.293V4.5a.5.5 0 0 1 1 0z" />
                      </svg>
                    </button>

                    <button
                      type="button"
                      className="btn btn-light"
                      onClick={() => this.sortResult("OrderName", false)}
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-arrow-up-square-fill"
                        viewBox="0 0 16 16"
                      >
                        <path d="M2 16a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2zm6.5-4.5V5.707l2.146 2.147a.5.5 0 0 0 .708-.708l-3-3a.5.5 0 0 0-.708 0l-3 3a.5.5 0 1 0 .708.708L7.5 5.707V11.5a.5.5 0 0 0 1 0z" />
                      </svg>
                    </button>
                  </div>
                  User
                </th>
                <th>Total order price</th>
                <th>Transaction date</th>
              </tr>
            </thead>
            <tbody>
              {orders.map((ord) => (
                <tr key={ord.idOrder}>
                  <td>{ord.idOrder}</td>
                  {users.map((usr) => {
                    if (usr.idUser == ord.idUser) {
                      return (
                        <td key={usr.idUser}>
                          {usr.firstName} {usr.lastName}{" "}
                        </td>
                      );
                    }
                  })}
                  <td>{ord.totalOrderPrice}</td>
                  <td>{ord.transactionDate}</td>
                  <td>
                    <button
                      type="button"
                      className="btn btn-light mr-1"
                      data-bs-toggle="modal"
                      data-bs-target="#exampleModal"
                      onClick={() => this.editClick(ord)}
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-pencil-square"
                        viewBox="0 0 16 16"
                      >
                        <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                        <path
                          fillRule="evenodd"
                          d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"
                        />
                      </svg>
                    </button>

                    <button
                      type="button"
                      className="btn btn-light mr-1"
                      onClick={() => this.deleteClick(ord.idOrder)}
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-trash-fill"
                        viewBox="0 0 16 16"
                      >
                        <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                      </svg>
                    </button>
                    <button
                      type="button"
                      className="btn btn-light mr-1"
                      onClick={() => this.openOrder(ord.idOrder)}
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-cart"
                        viewBox="0 0 16 16"
                      >
                        <path d="M0 1.5A.5.5 0 0 1 .5 1H2a.5.5 0 0 1 .485.379L2.89 3H14.5a.5.5 0 0 1 .491.592l-1.5 8A.5.5 0 0 1 13 12H4a.5.5 0 0 1-.491-.408L2.01 3.607 1.61 2H.5a.5.5 0 0 1-.5-.5zM3.102 4l1.313 7h8.17l1.313-7H3.102zM5 12a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm7 0a2 2 0 1 0 0 4 2 2 0 0 0 0-4zm-7 1a1 1 0 1 1 0 2 1 1 0 0 1 0-2zm7 0a1 1 0 1 1 0 2 1 1 0 0 1 0-2z" />
                      </svg>
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : null}

        {/* OrderItem table */}
        {WhichTab == 1 ? (
          <table className="table table-striped">
            <thead>
              <tr>
                <th>IDOrderItem</th>
                <th>Product</th>
                <th>OrderAmount</th>
                <th>OrderPrice</th>
              </tr>
            </thead>
            <tbody>
              {orderItems.map((ordit) => (
                <tr key={ordit.idOrderItem}>
                  <td>{ordit.idOrderItem}</td>
                  {products.map((pr) => {
                    if (pr.idProduct == ordit.idProduct) {
                      return <td key={pr.idProduct}>{pr.productName}</td>;
                    }
                  })}
                  <td>{ordit.orderAmount}</td>
                  <td>{ordit.orderPrice}</td>
                  <td>
                    <button
                      type="button"
                      className="btn btn-light mr-1"
                      data-bs-toggle="modal"
                      data-bs-target="#orderModal"
                      onClick={() => this.editOrderClick(ordit)}
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-pencil-square"
                        viewBox="0 0 16 16"
                      >
                        <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                        <path
                          fillRule="evenodd"
                          d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"
                        />
                      </svg>
                    </button>

                    <button
                      type="button"
                      className="btn btn-light mr-1"
                      onClick={() => this.deleteOrderClick(ordit.idOrderItem)}
                    >
                      <svg
                        xmlns="http://www.w3.org/2000/svg"
                        width="16"
                        height="16"
                        fill="currentColor"
                        className="bi bi-trash-fill"
                        viewBox="0 0 16 16"
                      >
                        <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                      </svg>
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        ) : null}

        {/* Orders modal */}
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
                <div className="input-group mb-3">
                  <span className="input-group-text">User</span>
                  <select
                    value={FirstName}
                    className="form-control"
                    id="exampleFormControlSelect1"
                    onChange={this.changeIDUser}
                  >
                    {users.map((usr) => (
                      <option key={usr.idUser} data-key={usr.idUser}>
                        {usr.firstName} {usr.lastName}{" "}
                      </option>
                    ))}
                  </select>
                </div>

                <div className="input-group mb-3">
                  <span className="input-group-text">Total order price</span>
                  <input
                    type="text"
                    className="form-control"
                    value={TotalOrderPrice}
                    onChange={this.changeTotalOrderPrice}
                  />
                </div>

                {IDOrder == 0 ? (
                  <button
                    type="button"
                    className="btn btn-primary float-start"
                    onClick={() => this.createClick()}
                  >
                    Create
                  </button>
                ) : null}

                {IDOrder != 0 ? (
                  <button
                    type="button"
                    className="btn btn-primary float-start"
                    onClick={() => this.updateClick()}
                  >
                    Update
                  </button>
                ) : null}
              </div>
            </div>
          </div>
        </div>

        {/* OrderItem modal */}
        <div
          className="modal fade"
          id="orderModal"
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
                {IDOrderItem != 0 ? (
                  <div className="input-group mb-3">
                    <span className="input-group-text">Order</span>
                    <input
                      type="text"
                      className="form-control"
                      value={IDOrder}
                      onChange={this.changeIDOrder}
                    />
                  </div>
                ) : null}

                <div className="input-group mb-3">
                  <span className="input-group-text">Product</span>
                  <select
                    value={ProductName}
                    className="form-control"
                    id="exampleFormControlSelect1"
                    onChange={this.changeOrderProduct}
                  >
                    {products.map((pr) => (
                      <option key={pr.idProduct} data-key={pr.idProduct} data-amount={pr.supplyKG}>
                        {pr.productName} : {pr.supplyKG}
                      </option>
                    ))}
                  </select>
                </div>

                <div className="input-group mb-3">
                  <span className="input-group-text">Item amount</span>
                  <input
                    type="text"
                    className="form-control"
                    value={OrderAmount}
                    onChange={this.changeOrderAmount}
                  />
                </div>

                <div className="input-group mb-3">
                  <span className="input-group-text">Price</span>
                  <input
                    type="number"
                    className="form-control"
                    value={OrderPrice}
                    onChange={this.changeOrderPrice}
                  />
                </div>

                {IDOrderItem == 0 ? (
                  <button
                    type="button"
                    className="btn btn-primary float-start"
                    onClick={() => this.createOrderClick()}
                  >
                    Create
                  </button>
                ) : null}

                {IDOrderItem != 0 ? (
                  <button
                    type="button"
                    className="btn btn-primary float-start"
                    onClick={() => this.updateOrderClick()}
                  >
                    Update
                  </button>
                ) : null}
              </div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
