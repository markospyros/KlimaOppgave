import React from "react";

const Form = (props) => {
  return (
    <form>
      <h2>{props.pageTitle}</h2>
      <div className="mt-3">
        <label className="form-label">Tittel</label>
        <input
          type="text"
          className={`form-control form-control-outline ${props.tittelInputState} border-3 shadow-none`}
          value={props.valueTittel}
          onChange={props.onChangeTittel}
          onFocus={props.onFocusTittel}
          onBlur={props.onBlurTittel}
        />
        <div className={`invalid-feedback d-${props.tittelErrorMessageStatus}`}>
          Tittel må fylles ut
        </div>
      </div>
      <div className="mt-3">
        <label className="form-label">Innhold</label>
        <textarea
          type="text"
          rows="5"
          className={`form-control form-control-outline ${props.innholdInputState} border-3 shadow-none`}
          value={props.valueInnhold}
          onChange={props.onChangeInnhold}
          onFocus={props.onFocusInnhold}
          onBlur={props.onBlurInnhold}
        ></textarea>
        <div
          className={`invalid-feedback d-${props.innholdErrorMessageStatus}`}
        >
          Innhold må fylles ut
        </div>
      </div>
      <div className="d-grid gap-2 mt-5">
        <button
          className="btn btn-primary"
          type="button"
          onClick={props.onSubmit}
        >
          {props.buttonTitle}
        </button>
      </div>
    </form>
  );
};

export default Form;
