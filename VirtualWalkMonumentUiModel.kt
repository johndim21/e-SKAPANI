package app.e_skapani.ui.virtual_walk.model

import androidx.annotation.DrawableRes
import androidx.annotation.StringRes
import app.e_skapani.R
import com.google.android.gms.maps.model.LatLng

enum class VirtualWalkMonumentUiItem(
    @DrawableRes val smallIcon: Int,
    @DrawableRes val bubbleIcon: Int,
    val bubblePositionHeight: Double,
    val bubblePositionWidth: Double,
    @StringRes val title: Int,
    @StringRes val smallDescription: Int,
    @StringRes val fullDescription: Int,
    val listOfImages: IntArray,
    val listOfImageDescription: IntArray,
    val mapPositionLatLng: LatLng,
    @StringRes val modelFileName: Int,
    val isArEnabled: Boolean,
    val is3dViewerEnabled: Boolean
) {
    ROTONTA(
        R.drawable.ic_rotonta,
        R.drawable.ic_bubble_rotonta,
        0.20,
        0.09,

        R.string.rotunda_title_label,
        R.string.rotunda_description_label,
        R.string.rotunda_information_label,
        intArrayOf(
            R.drawable.rotunda1
        ),
        intArrayOf(
            R.string.empty
        ),
        LatLng(40.63327816029945, 22.953000841602744),
        R.string.rotunda_model_name,
        false,
        false
    ),
    KAMARA(
        R.drawable.ic_kamara,
        R.drawable.ic_bubble_kamara,
        0.25,
        0.18,
        R.string.arch_of_galerius_title_label,
        R.string.arch_of_galerius_description_label,
        R.string.arch_of_galerius_information_label,
        intArrayOf(
            R.drawable.kamara1,
            R.drawable.kamara2,
            R.drawable.kamara3,
            R.drawable.kamara4,
            R.drawable.kamara5,
            R.drawable.kamara6
        ),
        intArrayOf(
            R.string.arch_of_galerius_image_1_label,
            R.string.arch_of_galerius_image_2_label,
            R.string.arch_of_galerius_image_3_label,
            R.string.arch_of_galerius_image_4_label,
            R.string.arch_of_galerius_image_5_label,
            R.string.arch_of_galerius_image_6_label
        ),
        LatLng(40.632197781890035, 22.951734651636418),
        R.string.arch_of_galerius_model_name,
        true,
        true
    ),
    IPONDROMOS(
        R.drawable.ic_ippodromos,
        R.drawable.ic_bubble_ippodromos,
        0.28,
        0.44,
        R.string.hippodrome_title_label,
        R.string.hippodrome_description_label,
        R.string.hippodrome_information_label,
        intArrayOf(
            R.drawable.ippodromos1,
            R.drawable.ippodromos2,
            R.drawable.ippodromos3,
            R.drawable.ippodromos4,
            R.drawable.ippodromos5,
            R.drawable.ippodromos6
        ),
        intArrayOf(
            R.string.hippodrome_image_1_label,
            R.string.hippodrome_image_2_label,
            R.string.hippodrome_image_3_label,
            R.string.hippodrome_image_4_label,
            R.string.hippodrome_image_5_label,
            R.string.hippodrome_image_6_label
        ),
        LatLng(40.62978736976924, 22.950620178068345),
        R.string.hippodrome_model_name,
        true,
        true
    ),
    OKTAGONO(
        R.drawable.ic_oktagwno,
        R.drawable.ic_bubble_oktagwno,
        0.45,
        0.19,
        R.string.octagon_title_label,
        R.string.octagon_description_label,
        R.string.octagon_information_label,
        intArrayOf(
            R.drawable.octagono1,
            R.drawable.octagono2,
            R.drawable.octagono3,
            R.drawable.octagono4,
            R.drawable.octagono5,
            R.drawable.toxo1
        ),
        intArrayOf(
            R.string.octagon_image_1_label,
            R.string.octagon_image_2_label,
            R.string.octagon_image_3_label,
            R.string.octagon_image_4_label,
            R.string.octagon_image_5_label,
            R.string.octagon_image_6_label
        ),
        LatLng(40.630067309188696, 22.948907841516046),
        R.string.octagon_model_name,
        true,
        true
    ),
    VASILIKI(
        R.drawable.ic_vasiliki,
        R.drawable.ic_bubble_vasiliki,
        0.375,
        0.475,
        R.string.vasiliki_title_label,
        R.string.vasiliki_description_label,
        R.string.vasiliki_information_label,
        intArrayOf(
            R.drawable.vasiliki1,
            R.drawable.vasiliki2,
            R.drawable.vasiliki3,
            R.drawable.vasiliki4,
            R.drawable.vasiliki5,
            R.drawable.vasiliki6

        ),
        intArrayOf(
            R.string.vasiliki_image_1_label,
            R.string.vasiliki_image_2_label,
            R.string.vasiliki_image_3_label,
            R.string.vasiliki_image_4_label,
            R.string.vasiliki_image_5_label,
            R.string.vasiliki_image_6_label
        ),
        LatLng(40.62987640955798, 22.949615514792168),
        R.string.vasiliki_model_name,
        true,
        true
    )
}