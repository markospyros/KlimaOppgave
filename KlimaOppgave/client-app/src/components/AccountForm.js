import React from "react";
import { Navigate, useNavigate } from "react-router-dom";

const AccountForm = (props) => {
  const navigate = useNavigate();

  return (
    <div className="container">
      <div className="row">
        <div className="col-md-5 m-auto">
          <div className="mt-5">
            <form onSubmit={props.onSubmit}>
              <h1 className="mb-5">{props.formTitle}</h1>
              <div className="mb-3">
                <label className="form-label">Brukernavn</label>
                <input
                  type="text"
                  className={`form-control form-control-outline ${props.brukernavnInputState} border-3 shadow-none`}
                  value={props.brukernavn}
                  onChange={props.onChangeBrukernavn}
                  onFocus={props.onFocusBrukerInput}
                  onBlur={props.onBlurBrukerInput}
                />
              </div>
              <div className={`invalid-feedback d-block`}>
                {props.brukernavnErrorMessage}
              </div>
              <div className="mb-3">
                <label className="form-label">Passord</label>
                <input
                  type="password"
                  className={`form-control form-control-outline ${props.passordInputState} border-3 shadow-none`}
                  value={props.passord}
                  onChange={props.onChangePassord}
                  onFocus={props.onFocusPassordInput}
                  onBlur={props.onBlurPassordInput}
                />
              </div>
              <div className={`invalid-feedback d-block`}>
                {props.passordErrorMessage}
              </div>
              <div className="mb-3 form-label">
                <label
                  className="form-label text-primary"
                  style={{ cursor: "pointer" }}
                  onClick={() => navigate(`/${props.path}`)}
                >
                  <u>{props.text}</u>
                </label>
              </div>
              <div className={`invalid-feedback d-block`}>
                {props.errorMessage}
              </div>
              <div className="d-grid gap-2 mt-5">
                <button className="btn btn-primary" onClick={props.onSubmit}>
                  {props.buttonStatus}
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default AccountForm;
