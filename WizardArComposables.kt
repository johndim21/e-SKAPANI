package app.e_skapani.ui.wizard_ar.composable

import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.runtime.livedata.observeAsState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.stringResource
import androidx.compose.ui.text.style.TextAlign
import app.e_skapani.R
import app.e_skapani.ui.base.compose.VerticalSpacer
import app.e_skapani.ui.composable.OnBackClicked
import app.e_skapani.ui.composable.bottom_nav_bars.OnArClicked
import app.e_skapani.ui.composable.general.EskapaniBody2Text
import app.e_skapani.ui.composable.general.EskapaniButton
import app.e_skapani.ui.composable.general.ScreenSetupWithoutImage
import app.e_skapani.ui.composable.toolbars.ToolbarBack
import app.e_skapani.ui.theme.*
import app.e_skapani.ui.wizard_ar.WizardArViewModel
import app.e_skapani.ui.wizard_ar.model.WizaArUiItem
import com.google.accompanist.pager.ExperimentalPagerApi
import com.google.accompanist.pager.HorizontalPager
import com.google.accompanist.pager.HorizontalPagerIndicator
import com.google.accompanist.pager.rememberPagerState


@Composable
fun WizardArScreen(viewModel: WizardArViewModel) {
    val wizardAr by viewModel.wizardArUi.observeAsState()

    WizardArContent(
        wizardAr = wizardAr,
        onBackClicked = viewModel::onBackClicked,
        onArClicked = viewModel::onArClicked
    )
}


@Composable
fun WizardArContent(
    wizardAr: WizaArUiItem?,
    onBackClicked: OnBackClicked,
    onArClicked: OnArClicked
) {
    ScreenSetupWithoutImage {
            Column(
                modifier = Modifier
                    .fillMaxSize()
                    .background(Color.Black),
            ) {

                ToolbarBack(onBackClicked = onBackClicked)
                
                VerticalSpacer(spacing = SpacingOctuple_128dp)
                if (wizardAr != null) {
                    WizardArImageCarousel(wizardAr, onArClicked)
                }
        }
    }
}

@OptIn(ExperimentalPagerApi::class)
@Composable
fun WizardArImageCarousel(wizardAr: WizaArUiItem, onArClicked: OnArClicked) {

    Column(modifier = Modifier
        .padding(horizontal = SpacingDefault_16dp),
        verticalArrangement = Arrangement.Center,
        horizontalAlignment = Alignment.CenterHorizontally) {

        val pagerState = rememberPagerState()

        HorizontalPager(
            modifier = Modifier,
            count = wizardAr.listOfImages.size,
            state = pagerState
        ) { page ->
            // Our page content
            Column {
                Image(
                    modifier = Modifier
                        .fillMaxWidth()
                        .aspectRatio(2f)
                        .padding(
                            bottom = SpacingQuarter_4dp
                        ),
                    painter = painterResource(id = wizardAr.listOfImages[page]),
                    contentDescription = ""
                )
            }
        }
        HorizontalPagerIndicator(
            pagerState = pagerState,
            modifier = Modifier
                .align(Alignment.CenterHorizontally)
                .padding(SpacingDefault_16dp),
            activeColor = ColorPagerIndicatorSelected,
            inactiveColor = ColorPagerIndicatorUnselected
        )

        VerticalSpacer(spacing = SpacingDefault_16dp)

        EskapaniBody2Text(
            modifier = Modifier
                .fillMaxWidth()
                .padding(horizontal = SpacingHalf_8dp)
                .padding(bottom = SpacingDefault_16dp),
            color = Color.White,
            text = stringResource(id = wizardAr.listOfDescription[pagerState.currentPage]),
            textAlign = TextAlign.Center
        )

        Box(modifier = Modifier.fillMaxSize(),
        contentAlignment = Alignment.BottomCenter){

        EskapaniButton(
            modifier = Modifier,
            text = stringResource(id = R.string.ok),
            onButtonClicked = onArClicked)
    }
    }
}