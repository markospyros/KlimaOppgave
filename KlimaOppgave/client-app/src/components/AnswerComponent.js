import axios from "axios";
import React, { useState } from "react";
import { BiEdit, BiTrash } from "react-icons/bi";
import { useNavigate } from "react-router-dom";
import ChangeAnswerModal from "./ChangeAnswerModal";

const AnswerComponent = (props) => {
  const [visibility, setVisibility] = useState("none");
  const [showModal, setShowModal] = useState(false);

  const onHoverAnswer = () => {
    setVisibility("block");
  };

  const onLeaveAnswer = () => {
    setVisibility("none");
  };

  const openModal = () => {
    setShowModal(true);
  };

  const closeModal = () => {
    setShowModal(false);
  };

  const navigate = useNavigate();

  const onDelete = (id) => {
    axios
      .delete(`/slettsvar/${id}`)
      .then(() => props.deleteAnswer(id))
      .catch((error) => {
        if (error.response.status === 401) {
          navigate("/login");
        }
      });
  };

  return (
    <div
      className="card-footer"
      onMouseEnter={onHoverAnswer}
      onMouseLeave={onLeaveAnswer}
    >
      <div className="row">
        <div className="col-9">
          <div>{props.svarInnhold}</div>
        </div>
        <div className={`col-3 text-end d-${visibility}`}>
          <BiEdit
            color="blue"
            size={20}
            className="mx-3"
            style={{ cursor: "pointer" }}
            onClick={openModal}
          />
          <BiTrash
            color="red"
            size={20}
            style={{ cursor: "pointer" }}
            onClick={() => onDelete(props.svarId)}
          />
        </div>
        <div className="text-end text-secondary">{props.svarDato}</div>
      </div>
      <ChangeAnswerModal
        svarId={props.svarId}
        svarInnhold={props.svarInnhold}
        svarDato={props.svarDato}
        innleggId={props.innleggId}
        showModal={showModal}
        updateAnswer={props.updateAnswer}
        closeModal={closeModal}
      />
    </div>
  );
};

export default AnswerComponent;
