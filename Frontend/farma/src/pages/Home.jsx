import React, { Component } from "react";
import { variables } from "../Variables";
import {
  MDBContainer,
  MDBRow,
  MDBCol,
  MDBCard,
  MDBCardBody,
  MDBCardImage,
  MDBCardFooter,
  MDBRipple,
  MDBBtn,
  MDBRange,
} from "mdb-react-ui-kit";
import ReactPaginate from "react-paginate";

export class Home extends Component {
  constructor(props) {
    super(props);

    this.state = {
      products: [],
      productTypes: [],
      classes: [],
      cart: [],
      Category: "",
      modalTitle: "",
      IDProduct: 0,
      IDProductType: "",
      IDClass: "",
      Class: "",
      IDOrigin: "",
      ProductName: "",
      SupplyKG: "",
      PriceKG: "",
      ProductPictureURL: "defaultProduct.jpg",
      ProductDescription: "",
      DiscountPercentage: "",
      PhotoPath: variables.PHOTO_URL,
      itemsPerPage: 4, // Number of items to display per page
      currentPage: 0, // Current page

      IDUser: localStorage.getItem("ID"),
      CartAmount: 1,
      CartPrice: "",

      ProductNameFilter: "",
      productsWithoutFilter: [],
    };
  }
  handlePageChange = ({ selected }) => {
    this.setState({ currentPage: selected });
  };

  handleRangeChange = (event) => {
    this.setState({ CartAmount: event.target.value });
  };

  createCartClick(Product, Price) {
    fetch(variables.API_URL + "cart", {
      method: "POST",
      headers: {
        Accept: "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        IDUser: localStorage.getItem("ID"),
        IDProduct: Product,
        CartAmount: this.state.CartAmount,
        CartPrice: Price * this.state.CartAmount,
      }),
    })
      .then((res) => "Created successfuly")
      .then(alert("Added"), localStorage.setItem("cartAmount", parseInt(localStorage.getItem("cartAmount")) + Price * this.state.CartAmount), this.refreshList());
  }

  FilterFn() {
    var ProductNameFilter = this.state.ProductNameFilter;
    var filteredData = this.state.productsWithoutFilter.filter(function (el) {
      return (
        el.productName
          .toString()
          .toLowerCase()
          .includes(ProductNameFilter.toString().trim().toLowerCase())
      );
    });
    this.setState({ products: filteredData });
    this.setState({ currentPage: 0 });
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

  changeProductNameFilter = (e) => {
    this.state.ProductNameFilter = e.target.value;
    this.FilterFn();
  };

  /* loopCart(prod) {
    return this.state.data.cart(e => cart.idProduct === prod.idProduct);
  } */

  refreshList() {
    fetch(variables.API_URL + "product")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        this.setState({ products: data, productsWithoutFilter: data });
      });
    fetch(variables.API_URL + "productType")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        this.setState({ productTypes: data });
      });
    fetch(variables.API_URL + "cart/user/" + localStorage.getItem("ID"), {
      headers: {
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then((response) => {
        return response.json();
      })
      .then((data) => {
        if (data != null) {
          console.log(data);
          this.setState({ cart: data });
        } else {
          this.setState({ cart: [] });
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
      fetch(variables.API_URL + "class")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        this.setState({ classes: data });
      });
  }

  componentDidMount() {
    if(localStorage.getItem("ID") == 'null'){
      window.location.assign("http://127.0.0.1:5173/visitor")
    }
    this.refreshList();
  }

  render() {
    const {
      products,
      productTypes,
      cart,
      classes,
      Class,
      Category,
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
      CartAmount,
      CartPrice,
      IDUser,
    } = this.state;
    const { itemsPerPage, currentPage } = this.state;
    const pageCount = Math.ceil(products.length / itemsPerPage);
    const slicedData = products.slice(
      currentPage * itemsPerPage,
      (currentPage + 1) * itemsPerPage
    );

    return (
      <div>
        <table className="table table-striped">
          <thead>
            <tr>
              <th>
                <div className="d-flex flex-row align-items-center">
                  <input
                    className="form-control m-2"
                    onChange={this.changeProductNameFilter}
                    placeholder="Search"
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
              </th>
            </tr>
          </thead>
        </table>
        {/* <div className="row">
          {products.map((prod) => (
            <div className="col-lg-4 col-md-6 mb-4" key={prod.idProduct}>
              <div className="card h-100">
                <img
                  src={PhotoPath + prod.productPictureURL}
                  className="card-img-top img-fluid mx-auto mt-3"
                  alt={prod.productName}
                  style={{ maxWidth: "200px" }}
                />
                <div className="card-body">
                  <h5 className="card-title text-center mt-3">
                    {prod.productName}
                  </h5>
                  <p className="card-text">{prod.productDescription}</p>
                </div>
                <ul className="list-group list-group-flush">
                  <li className="list-group-item">
                    <strong>IDProduct:</strong> {prod.idProduct}
                  </li>
                  <li className="list-group-item">
                    <strong>IDProductType:</strong> {prod.idProductType}
                  </li>
                  <li className="list-group-item">
                    <strong>IDClass:</strong> {prod.idClass}
                  </li>
                  <li className="list-group-item">
                    <strong>IDOrigin:</strong> {prod.idOrigin}
                  </li>
                  <li className="list-group-item">
                    <strong>Supply(KG):</strong> {prod.supplyKG}
                  </li>
                  <li className="list-group-item">
                    <strong>Price(KG):</strong> {prod.priceKG}
                  </li>
                  <li className="list-group-item">
                    <strong>Discount(%):</strong> {prod.discountPercentage}
                  </li>
                </ul>
                <div className="card-footer">
                  <button className="btn btn-primary btn-block">
                    Add to Cart
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div> */}

        <MDBContainer fluid className="my-0 text-center">


          <MDBRow className="row-cols-1 row-cols-md-3 g-4">
            {slicedData.map((prod) => (
              <MDBCol md="12" lg="3" className="mb-4" key={prod.idProduct}>
                <MDBCard className="h-100" key={prod.idProduct}>
                  <MDBRipple
                    rippleColor="light"
                    rippleTag="div"
                    className="bg-image rounded hover-zoom"
                  >
                    <MDBCardImage
                      src={PhotoPath + prod.productPictureURL}
                      fluid
                      className="w-70"
                    />
                    <a href="#!">
                      <div className="mask">
                        <div className="d-flex justify-content-start align-items-end h-100">
                          <h5>
                            {prod.discountPercentage != 0 ? (
                              <span className="badge bg-danger ms-2">
                                -{prod.discountPercentage}%
                              </span>
                            ) : null}
                          </h5>
                        </div>
                      </div>
                      <div className="hover-overlay">
                        <div
                          className="mask"
                          style={{
                            backgroundColor: "rgba(251, 251, 251, 0.15)",
                          }}
                        ></div>
                      </div>
                    </a>
                  </MDBRipple>
                  <MDBCardBody>
                    <div className="text-reset">
                      <h5 className="card-title mb-3">{prod.productName}</h5>
                    </div>
                    <a href="#!" className="text-reset">
                      <div>
                        {productTypes.map((prt) => {
                          if (prt.idProductType == prod.idProductType) {
                            return (
                              <p key={prt.idProductType}>{"Type: "}{prt.category}</p>
                            );
                          }
                        })}
                        {classes.map((cls) => {
                          if (cls.idClass == prod.idClass) {
                            return (
                              <p key={cls.idClass}>{"Class: "}{cls.class}</p>
                            );
                          }
                        })}
                      </div>
                    </a>
                    <h6 className="mb-3">
                      {prod.discountPercentage == 0 ? (
                        <p>{prod.priceKG} RSD</p>
                      ) : null}
                      {prod.discountPercentage != 0 ? (
                        <>
                          <s>{prod.priceKG}</s>
                          <strong className="ms-2 text-danger">
                            {prod.priceKG -
                              (prod.priceKG * prod.discountPercentage) / 100}
                          </strong>{" "}
                          RSD
                        </>
                      ) : null}
                    </h6>
                  </MDBCardBody>
                  {cart.find((obj) => obj.idProduct === prod.idProduct) ? (
                    <MDBCardFooter>
                      <MDBRange
                        defaultValue={0}
                        value={this.CartAmount}
                        min="1"
                        max={prod.supplyKG}
                        step="1"
                        id="customRange3"
                        label="Kolicina(*100g/komada)"
                        onChange={this.handleRangeChange}
                        disabled
                      />
                      <MDBBtn
                        disabled
                        color="success"
                        onClick={() =>
                          this.createCartClick(
                            prod.idProduct,
                            prod.priceKG -
                              (prod.priceKG * prod.discountPercentage) / 100
                          )
                        }
                      >
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          width="22"
                          height="22"
                          fill="currentColor"
                          className="bi bi-cart-plus"
                          viewBox="0 0 16 16"
                        >
                          <path d="M9 5.5a.5.5 0 0 0-1 0V7H6.5a.5.5 0 0 0 0 1H8v1.5a.5.5 0 0 0 1 0V8h1.5a.5.5 0 0 0 0-1H9V5.5z" />
                          <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1H.5zm3.915 10L3.102 4h10.796l-1.313 7h-8.17zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z" />
                        </svg>
                      </MDBBtn>{" "}Proizvod je vec u korpi
                    </MDBCardFooter>
                  ) : (
                    <MDBCardFooter>
                      <MDBRange
                        defaultValue={0}
                        value={this.CartAmount}
                        min="1"
                        max={prod.supplyKG}
                        step="1"
                        id="customRange3"
                        label="Kolicina(*100g/komada)"
                        onChange={this.handleRangeChange}
                      />
                      <MDBBtn
                        color="success"
                        onClick={() =>
                          this.createCartClick(
                            prod.idProduct,
                            prod.priceKG -
                              (prod.priceKG * prod.discountPercentage) / 100
                          )
                        }
                      >
                        <svg
                          xmlns="http://www.w3.org/2000/svg"
                          width="22"
                          height="22"
                          fill="currentColor"
                          className="bi bi-cart-plus"
                          viewBox="0 0 16 16"
                        >
                          <path d="M9 5.5a.5.5 0 0 0-1 0V7H6.5a.5.5 0 0 0 0 1H8v1.5a.5.5 0 0 0 1 0V8h1.5a.5.5 0 0 0 0-1H9V5.5z" />
                          <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1H.5zm3.915 10L3.102 4h10.796l-1.313 7h-8.17zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0zm7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0z" />
                        </svg>
                      </MDBBtn>
                    </MDBCardFooter>
                  )}
                </MDBCard>
              </MDBCol>
            ))}
          </MDBRow>
          {/* Render pagination component */}
          <div style={{ display: "flex", justifyContent: "center" }}>
            <nav>
              <ul className="pagination-lg">
                <ReactPaginate
                  pageCount={pageCount}
                  marginPagesDisplayed={2}
                  pageRangeDisplayed={5}
                  onPageChange={this.handlePageChange}
                  previousLabel="Previous"
                  nextLabel="Next"
                  breakLabel="..."
                  containerClassName="pagination"
                  activeClassName="active"
                  pageLinkClassName="page-link"
                  previousLinkClassName="page-link"
                  nextLinkClassName="page-link"
                  disabledClassName="disabled"
                />
              </ul>
            </nav>
          </div>
        </MDBContainer>
        <div
          className="bg-image row"
          style={{
            backgroundImage: "url(src/assets/images/MainBackgroundBottom.png)",
            backgroundRepeat: "repeat-x",
            height: "250px",
          }}
        ></div>
      </div>
    );
  }
}
