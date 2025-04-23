import { useFormik } from "formik";
import * as Yup from "yup";
import { AuctionInfoCreateRequest } from "../../types/auctionInfo";
import { useParams } from "react-router-dom";
import api from "../../services/api";

export default function StartAuctionForm() {
  const carId = useParams().carId as string;

  const formik = useFormik({
    initialValues: {
      startPrice: "",
      buyoutPrice: "",
      startDate: "",
      endDate: "",
    },
    validationSchema: Yup.object({
      startPrice: Yup.number()
        .required("Start price is required")
        .positive("Must be positive"),
      buyoutPrice: Yup.number()
        .required("Buyout price is required")
        .moreThan(
          Yup.ref("startPrice"),
          "Buyout must be greater than start price"
        ),
      startDate: Yup.date().required("Start date is required"),
      endDate: Yup.date()
        .required("End date is required")
        .min(Yup.ref("startDate"), "End date must be after start date"),
    }),
    onSubmit: async (values, { resetForm }) => {
      try {
        const auctionInfo: AuctionInfoCreateRequest = {
          carId: carId,
          startPrice: parseFloat(values.startPrice),
          buyoutPrice: parseFloat(values.buyoutPrice),
          startDate: new Date(values.startDate),
          endDate: new Date(values.endDate),
        };
        await api.AuctionInfo.createAuctionInfo(auctionInfo);
        alert("Auction info submited successfully!");
        resetForm();
      } catch (error) {
        console.error("Error in submiting auction info:", error);
        alert("Failed to submit auction info");
      }
    },
  });

  return (
    <form
      onSubmit={formik.handleSubmit}
      className="max-w-md mx-auto p-4 space-y-4 bg-white shadow rounded"
    >
      <div>
        <label
          htmlFor="startPrice"
          className="block text-sm font-medium text-gray-700"
        >
          Start Price
        </label>
        <div className="mt-1 flex items-center">
          <input
            id="startPrice"
            name="startPrice"
            type="number"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values.startPrice}
            className="flex-1 rounded border border-gray-300 p-2"
          />
          <span className="ml-2 text-gray-600">$</span>
        </div>
        {formik.touched.startPrice && formik.errors.startPrice && (
          <p className="text-red-500 text-sm">{formik.errors.startPrice}</p>
        )}
      </div>

      <div>
        <label
          htmlFor="buyoutPrice"
          className="block text-sm font-medium text-gray-700"
        >
          Buyout Price
        </label>
        <div className="mt-1 flex items-center">
          <input
            id="buyoutPrice"
            name="buyoutPrice"
            type="number"
            onChange={formik.handleChange}
            onBlur={formik.handleBlur}
            value={formik.values.buyoutPrice}
            className="flex-1 rounded border border-gray-300 p-2"
          />
          <span className="ml-2 text-gray-600">$</span>
        </div>
        {formik.touched.buyoutPrice && formik.errors.buyoutPrice && (
          <p className="text-red-500 text-sm">{formik.errors.buyoutPrice}</p>
        )}
      </div>

      <div>
        <label
          htmlFor="startDate"
          className="block text-sm font-medium text-gray-700"
        >
          Start Date
        </label>
        <input
          id="startDate"
          name="startDate"
          type="datetime-local"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          value={formik.values.startDate}
          className="mt-1 block w-full rounded border border-gray-300 p-2"
        />
        {formik.touched.startDate && formik.errors.startDate && (
          <p className="text-red-500 text-sm">{formik.errors.startDate}</p>
        )}
      </div>

      <div>
        <label
          htmlFor="endDate"
          className="block text-sm font-medium text-gray-700"
        >
          End Date
        </label>
        <input
          id="endDate"
          name="endDate"
          type="datetime-local"
          onChange={formik.handleChange}
          onBlur={formik.handleBlur}
          value={formik.values.endDate}
          className="mt-1 block w-full rounded border border-gray-300 p-2"
        />
        {formik.touched.endDate && formik.errors.endDate && (
          <p className="text-red-500 text-sm">{formik.errors.endDate}</p>
        )}
      </div>

      <button
        type="submit"
        className="w-full bg-blue-600 text-white py-2 px-4 rounded hover:bg-blue-700 transition"
      >
        Start Auction
      </button>
    </form>
  );
}
