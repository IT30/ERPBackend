import React, { Component } from "react";
import { MDBContainer, MDBTypography } from "mdb-react-ui-kit";

export class About extends Component {
  render() {
    return (
      <div className="pb-4">
          <MDBTypography
            tag="div"
            className="display-1 pb-3 mb-3 "
          >
            Thank you for visiting
          </MDBTypography><MDBTypography
            tag="div"
            className="display-5 pb-3 mb-3 border-bottom"
          >
            We hope you enjoyed
          </MDBTypography>
          
          <img
            src="src\assets\images\honestwork.jpg"
            className="img-fluid shadow-4"
            alt="..."
          />
      </div>
    );
  }
}
