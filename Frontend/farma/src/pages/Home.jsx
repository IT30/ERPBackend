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
} from "mdb-react-ui-kit";

export class Home extends Component {
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
        el.idProduct
          .toString()
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
        <table className="table table-striped">
          <thead>
            <tr>
              <th>
                <div className="d-flex flex-row align-items-center">
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
                <div className="d-flex flex-row align-items-center">
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
                Product name
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

        <MDBContainer fluid className="my-5 text-center">
          <h4 className="mt-4 mb-5">
            <strong>Products</strong>
          </h4>

          <MDBRow className='row-cols-1 row-cols-md-3 g-4'>
            {products.map((prod) => (
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
                      className="w-100"
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
                    <a href="#!" className="text-reset">
                      <h5 className="card-title mb-3">{prod.productName}</h5>
                    </a>
                    <a href="#!" className="text-reset">
                      <p>Category</p>
                    </a>
                    <h6 className="mb-3">
                      <s>{prod.priceKG}</s>
                      {prod.discountPercentage != 0 ? (
                        <strong className="ms-2 text-danger">
                          {prod.priceKG -
                            (prod.priceKG * prod.discountPercentage) / 100}
                        </strong>
                      ) : null}
                    </h6>
                  </MDBCardBody>
                  <MDBCardFooter>
            <small className='text-muted'>Last updated 3 mins ago</small>
          </MDBCardFooter>
                </MDBCard>
              </MDBCol>
            ))}
          </MDBRow>
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
