import React from "react";

const Form = (props) => {
  return (
    <form>
      <h2>{props.pageTitle}</h2>
      <div className="mt-3">
        <label className="form-label">Tittel</label>
        <input
          type="text"
          className="form-control"
          value={props.value}
          onChange={props.onChangeTittel}
        />
      </div>
      <div className="mt-3">
        <label className="form-label">Innhold</label>
        <textarea
          rows="5"
          className="form-control"
          value={props.value}
          onChange={props.onChangeInnhold}
        ></textarea>
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
