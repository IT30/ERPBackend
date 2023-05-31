import React, { Component } from "react";
import { MDBContainer, MDBTypography } from "mdb-react-ui-kit";

export class About extends Component {
  render() {
    return (
      <MDBContainer className="mt-2 mb-3 pb-4 pt-2">
        <MDBTypography tag='div' className='display-1 pb-3 mb-3 border-bottom'>Thank you for visiting</MDBTypography>
        <img
          src="src\assets\images\honestwork.jpg"
          className="img-fluid shadow-4"
          alt="..."
        />
      </MDBContainer>
    );
  }
}
