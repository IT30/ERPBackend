import React, { Component } from "react";
import { variables } from "../Variables";

export class Product extends Component {
  constructor(props) {
    super(props);

    this.state = {
      products: [],
      modalTitle: "",
      IDProduct: 0,
      IDProductType: "",
      IDClass: "",
      IDOrigin: "",
      ProductName: "",
      SupplyKG: "",
      PriceKG: "",
      ProductPictureURL: "defaultProduct.jpg",
      ProductDescription: "",
      DiscountPercentage: "",
      PhotoPath: variables.PHOTO_URL,

      IDProductFilter: "",
      ProductNameFilter: "",
      productsWithoutFilter: [],
    };
  }

  FilterFn() {
    var IDProductFilter = this.state.IDProductFilter;
    var ProductNameFilter = this.state.ProductNameFilter;

    var filteredData = this.state.productsWithoutFilter.filter(function (el) {
      return (
        el.idProduct.toString()
          .toLowerCase()
          .includes(IDProductFilter.toString().trim().toLowerCase()) &&
        el.productName
          .toString()
          .toLowerCase()
          .includes(ProductNameFilter.toString().trim().toLowerCase())
      );
    });

    this.setState({ products: filteredData });
  }

  sortResult(prop, asc) {
    var sortedData = this.state.productsWithoutFilter.sort(function (a, b) {
      if (asc) {
        return a[prop] > b[prop] ? 1 : a[prop] < b[prop] ? -1 : 0;
      } else {
        return b[prop] > a[prop] ? 1 : b[prop] < a[prop] ? -1 : 0;
      }
    });

    this.setState({ products: sortedData });
  }

  changeIDProductFilter = (e) => {
    this.state.IDProductFilter = e.target.value;
    this.FilterFn();
  };
  changeProductNameFilter = (e) => {
    this.state.ProductNameFilter = e.target.value;
    this.FilterFn();
  };

  refreshList() {
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

  changeIDProductType = (e) => {
    this.setState({ IDProductType: e.target.value });
  };

  changeIDClass = (e) => {
    this.setState({ IDClass: e.target.value });
  };

  changeIDOrigin = (e) => {
    this.setState({ IDOrigin: e.target.value });
  };

  changeProductName = (e) => {
    this.setState({ ProductName: e.target.value });
  };

  changeSupplyKG = (e) => {
    this.setState({ SupplyKG: e.target.value });
  };

  changePriceKG = (e) => {
    this.setState({ PriceKG: e.target.value });
  };

  changeProductDescription = (e) => {
    this.setState({ ProductDescription: e.target.value });
  };

  changeDiscountPercentage = (e) => {
    this.setState({ DiscountPercentage: e.target.value });
  };

  addClick() {
    this.setState({
      modalTitle: "Add Product",
      IDProductType: "",
      IDClass: "",
      IDOrigin: "",
      ProductName: "",
      SupplyKG: "",
      PriceKG: "",
      ProductPictureURL: "defaultProduct.jpg",
      ProductDescription: "",
      DiscountPercentage: "",
    });
  }
  editClick(prod) {
    this.setState({
      modalTitle: "Edit Product",
      IDProduct: prod.idProduct,
      IDProductType: prod.idProductType,
      IDClass: prod.idClass,
      IDOrigin: prod.idOrigin,
      ProductName: prod.productName,
      SupplyKG: prod.supplyKG,
      PriceKG: prod.priceKG,
      ProductPictureURL: prod.productPictureURL,
      ProductDescription: prod.productDescription,
      DiscountPercentage: prod.discountPercentage,
    });
  }

  createClick() {
    fetch(variables.API_URL + "product", {
      method: "POST",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDProductType: this.state.IDProductType,
        IDClass: this.state.IDClass,
        IDOrigin: this.state.IDOrigin,
        ProductName: this.state.ProductName,
        SupplyKG: this.state.SupplyKG,
        PriceKG: this.state.PriceKG,
        ProductPictureURL: "defaultProduct.jpg",
        ProductDescription: this.state.ProductDescription,
        DiscountPercentage: this.state.DiscountPercentage,
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
    fetch(variables.API_URL + "product", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDProduct: this.state.IDProduct,
        IDProductType: this.state.IDProductType,
        IDClass: this.state.IDClass,
        IDOrigin: this.state.IDOrigin,
        ProductName: this.state.ProductName,
        SupplyKG: this.state.SupplyKG,
        PriceKG: this.state.PriceKG,
        ProductPictureURL: this.state.ProductPictureURL,
        ProductDescription: this.state.ProductDescription,
        DiscountPercentage: this.state.DiscountPercentage,
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
      fetch(variables.API_URL + "product/" + id, {
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
        this.setState({ ProductPictureURL: data });
      });
  };

  render() {
    const {
      products,
      modalTitle,
      IDProduct,
      IDProductType,
      IDClass,
      IDOrigin,
      ProductName,
      SupplyKG,
      PriceKG,
      ProductPictureURL,
      ProductDescription,
      DiscountPercentage,
      PhotoPath,
    } = this.state;

    return (
      <div>
        <button
          type="button"
          className="btn btn-primary m-2 float-end"
          data-bs-toggle="modal"
          data-bs-target="#exampleModal"
          onClick={() => this.addClick()}
        >
          Add Product
        </button>
        <table className="table table-striped">
          <thead>
            <tr>
              <th>
                <div className="d-flex flex-row">
                  <input
                    className="form-control m-2"
                    onChange={this.changeIDProductFilter}
                    placeholder="Filter"
                  />

                  <button
                    type="button"
                    className="btn btn-light"
                    onClick={() => this.sortResult("idProduct", true)}
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
                    onClick={() => this.sortResult("idProduct", false)}
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
                IDProduct
              </th>
              <th>
                <div className="d-flex flex-row">
                  <input
                    className="form-control m-2"
                    onChange={this.changeProductNameFilter}
                    placeholder="Filter"
                  />

                  <button
                    type="button"
                    className="btn btn-light"
                    onClick={() => this.sortResult("productName", true)}
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
                    onClick={() => this.sortResult("productName", false)}
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
                IDProductType
              </th>
              <th>IDClass</th>
              <th>IDOrigin</th>
              <th>Product name</th>
              <th>Supply(KG)</th>
              <th>Price(KG)</th>
              <th>Product description</th>
              <th>Discount(%)</th>
            </tr>
          </thead>
          <tbody>
            {products.map((prod) => (
              <tr key={prod.idProduct}>
                <td>{prod.idProduct}</td>
                <td>{prod.idProductType}</td>
                <td>{prod.idClass}</td>
                <td>{prod.idOrigin}</td>
                <td>{prod.productName}</td>
                <td>{prod.supplyKG}</td>
                <td>{prod.priceKG}</td>
                <td>{prod.productDescription}</td>
                <td>{prod.discountPercentage}</td>
                <td>
                  <button
                    type="button"
                    className="btn btn-light mr-1"
                    data-bs-toggle="modal"
                    data-bs-target="#exampleModal"
                    onClick={() => this.editClick(prod)}
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
                    onClick={() => this.deleteClick(prod.idProduct)}
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
                  <span className="input-group-text">Product Type</span>
                  <input
                    type="text"
                    className="form-control"
                    value={IDProductType}
                    onChange={this.changeIDProductType}
                  />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Class</span>
                  <input
                    type="text"
                    className="form-control"
                    value={IDClass}
                    onChange={this.changeIDClass}
                  />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Origin</span>
                  <input
                    type="text"
                    className="form-control"
                    value={IDOrigin}
                    onChange={this.changeIDOrigin}
                  />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Product Name</span>
                  <input
                    type="text"
                    className="form-control"
                    value={ProductName}
                    onChange={this.changeProductName}
                  />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Supply in KG</span>
                  <input
                    type="text"
                    className="form-control"
                    value={SupplyKG}
                    onChange={this.changeSupplyKG}
                  />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Price per KG</span>
                  <input
                    type="text"
                    className="form-control"
                    value={PriceKG}
                    onChange={this.changePriceKG}
                  />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Product Description</span>
                  <input
                    type="text"
                    className="form-control"
                    value={ProductDescription}
                    onChange={this.changeProductDescription}
                  />
                </div>
                <div className="input-group mb-3">
                  <span className="input-group-text">Discount percentage</span>
                  <input
                    type="text"
                    className="form-control"
                    value={DiscountPercentage}
                    onChange={this.changeDiscountPercentage}
                  />
                </div>
                <div className="p-2 w-50 bd-highlight">
                    <img
                      width="250px"
                      height="250px"
                      src={PhotoPath + ProductPictureURL}
                    />
                    <input
                      className="m-2"
                      type="file"
                      onChange={this.imageUpload}
                    />
                  </div>

                {IDProduct == 0 ? (
                  <button
                    type="button"
                    className="btn btn-primary float-start"
                    onClick={() => this.createClick()}
                  >
                    Create
                  </button>
                ) : null}

                {IDProduct != 0 ? (
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
      </div>
    );
  }
}
