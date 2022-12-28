import React from "react";

const PostComponent = (props) => {
  return (
    <>
      <div className="card-body">
        <p className="card-text fs-5 text-start">{props.innhold}</p>
        <p className="card-text text-end text-secondary">{props.dato}</p>
      </div>
    </>
  );
};

export default PostComponent;
